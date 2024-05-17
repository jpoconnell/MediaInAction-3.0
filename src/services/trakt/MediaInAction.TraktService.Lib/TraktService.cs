using System.Threading.Tasks;
using TraktNet.Objects.Get.Calendars;
using TraktNet.Objects.Get.Collections;
using TraktNet.Objects.Get.History;
using TraktNet.Objects.Get.Shows;
using TraktNet.Objects.Get.Episodes;
using TraktNet.Objects.Get.Movies;
using Microsoft.Extensions.Logging;
using TraktNet;
using System;
using System.Collections.Generic;
using System.Linq;
using MediaInAction.TraktService.Config;
using MediaInAction.TraktService.MovieNs.Dtos;
using MediaInAction.TraktService.ShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using TraktNet.Enums;
using TraktNet.Objects.Authentication;
using TraktNet.Parameters;

namespace MediaInAction.TraktService.Lib
{
    public class TraktService : ITraktService
    {
        private TraktClient _traktClient;
        private readonly ITraktShowLibService _showService;
        private readonly ITraktMovieLibService _movieService;
        private readonly ITraktEpisodeLibService _episodeService;
        private readonly ILogger<TraktService> _logger;
        private readonly ServicesConfiguration _traktConfig;
        
        public TraktService(
            ITraktShowLibService showService,
            ITraktMovieLibService movieService,
            ITraktEpisodeLibService episodeService,
            ServicesConfiguration traktConfig,
            ILogger<TraktService> logger)
        {
            _showService = showService;
            _movieService = movieService;
            _episodeService = episodeService;
            _logger = logger;
            _traktConfig = traktConfig;
            _traktClient = new TraktClient( _traktConfig.ClientId, _traktConfig.ClientSecret);
            _traktClient.Authorization = TraktAuthorization.CreateWith(_traktConfig.AccessToken, _traktConfig.RefreshToken);
        }
        
        private TraktClient GetClient()
        {
            // setup Trakt Client
            var traktToken = _traktConfig.AccessToken;
            var traktRefreshToken = _traktConfig.RefreshToken;
            var traktClientId = _traktConfig.ClientId;
            var traktClientSecret = _traktConfig.ClientSecret;

            _traktClient = new TraktClient( traktClientId, traktClientSecret);
            _traktClient.Authorization = TraktAuthorization.CreateWith(traktToken, traktRefreshToken);
            return _traktClient;
        }

        public async Task GetWatchedShows()
        {
            _logger.LogInformation("GetWatchedShows Service Started");
            var historyItemType = new TraktSyncItemType();
            historyItemType = TraktSyncItemType.Show;
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-2);
            endAt = DateTime.UtcNow;

            var pagedParameters = new TraktPagedParameters(1,600);
            
            var result = await _traktClient.Sync.GetWatchedHistoryAsync(historyItemType, 
                null, startAt,endAt, extendedInfo, pagedParameters);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Watched Shows Pull count from Trakt:" + result.Value.Count().ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Episode != null)
                    {
                        await AddWatchedHistoryShow(traktItem);
                    }
                }
            }
        }

        public async Task GetWatchedMovies()
        {
            var historyItemType = new TraktSyncItemType();
            historyItemType = TraktSyncItemType.Movie;
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-3);
            endAt = DateTime.UtcNow;
            var result = await _traktClient.Sync.GetWatchedHistoryAsync(historyItemType, 
                null, startAt,endAt, extendedInfo);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Collected Watched Movies:" + result.Value.Count().ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedHistoryShow(traktItem);
                    }
                }
            }
        }

        public async Task GetMovieCollection()
        {
            _logger.LogInformation("GetMovieCollection Service Start");
            var result = await _traktClient.Sync.GetCollectionMoviesAsync();   

            if (result.IsSuccess)
            { 
                _logger.LogInformation("Movies Collected:" + result.Value.ToString());
                foreach (var traktMovie in result.Value)
                {
                    await AddCollection(traktMovie);
                }
            }
            else 
            {
                _logger.LogError("Unsuccessful response from Trakt");
            }
        }

        public async Task GetShowCollection()
        {
            _logger.LogInformation("GetShowCollection started");
            try
            {
                var result = await _traktClient.Sync.GetCollectionShowsAsync();   
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Collected from trakt:" + result.Value.Count().ToString());
                    foreach (var traktShow in result.Value)
                    {
                        await AddShowCollection(traktShow);
                    }
                    _logger.LogInformation("GetShowCollection finished");
                }
                else 
                {
                    _logger.LogDebug("Unsuccessful response from Trakt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("GetShowCollection Failed:" + ex.Message);
            }
        }

        public async Task SyncCalendarAsync()
        {
            var myDate = DateTime.Now.AddDays(-7);

            try
            {
                var result = await _traktClient.Calendar.GetUserShowsAsync(myDate, 14);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Calendar Items Found:" + result.Value.Count().ToString());
                    foreach (var traktShow in result.Value)
                    {
                        await this.AddCalendar(traktShow);
                    }
                }
                else
                {
                    _logger.LogError("Unsuccessful response from Trakt");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("SyncCalendarAsync Failed:" + ex.Message);
            }
        }
        
        public async Task GetWatchedList(string type)
        {
            _logger.LogInformation("GetWatchedList for "+ type + " Service Started");
            var historyItemType = new TraktSyncItemType();
            if (type == "Shows")
            {
                historyItemType = TraktSyncItemType.Show;
            }
            else
            {
                historyItemType = TraktSyncItemType.Movie;
            }
          
            var startAt = new DateTime();
            var endAt = new DateTime(); 
            var extendedInfo = new TraktExtendedInfo();
            extendedInfo.Full = true;
            startAt = DateTime.UtcNow.AddMonths(-3);
            endAt = DateTime.UtcNow;

            var pagedParameters = new TraktPagedParameters(1,500);
            
            var result = await _traktClient.Sync.GetWatchlistAsync(historyItemType,
                null, extendedInfo, pagedParameters);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Shows Collected:" + result.Value.ToString());
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedListMovie(traktItem.Movie);
                    }
                    if (traktItem.Show != null)
                    {
                        await AddWatchedListShow(traktItem.Show);
                    }
                }
            }
        }
        
        public async Task GetLastActivities()
        {
            var pagedParameters = new TraktPagedParameters(1,500);

            var result = await _traktClient.Sync.GetLastActivitiesAsync();
            
            if (result.IsSuccess)
            {
                /*
                foreach (var traktItem in result.Value)
                {
                    if (traktItem.Movie != null)
                    {
                        await AddWatchedListMovie(traktItem.Movie);
                    }
                    if (traktItem.Show != null)
                    {
                        await AddWatchedListShow(traktItem.Show);
                    }
                }
                */
            }
        }

        private async Task AddShowCollection(ITraktCollectionShow traktShow)
        {
            try {
                var show = ParseCollectionShow(traktShow);
                await _showService.UpdateAddFromDto(show);
            }
            catch 
            {
                _logger.LogError("ERROR: AddShowCollection");
            }
        }

        private async Task AddCalendar(ITraktCalendarShow traktCalendarShow)
        {
            var calendarShow = ParseCalendarShow(traktCalendarShow);
            await _showService.UpdateAddFromDto(calendarShow);
            foreach (var episode in calendarShow.CollectionEpisodeDtos)
            {
                episode.ShowSlug = calendarShow.Slug;
                await _episodeService.UpdateAddFromDto(episode);
            }
        }

        private async Task AddCollection(ITraktCollectionMovie traktMovie)
        {
            try
            {
                var movie = ParseCollectionMovie(traktMovie);
                if (movie != null)
                {                
                    await _movieService.UpdateAddFromDto(movie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        /*
        public async Task GetSeasons()
        {
            _logger.LogInformation("TraktService.GetSeasons started");
           // var episodeList = null; //await _showService.GetNoDateEpisodes();
           var traktShowSeasonList = new List<TraktShowSeasonDto>();
           var prevShow = "";
           var extendedInfo = new TraktExtendedInfo();
           extendedInfo.Full = true;
           _logger.LogInformation("TraktService.GetSeasons episode Count is "+ episodeList.Count.ToString());
           foreach (var episodeDto in episodeList)
           {
               if (episodeDto.AiredDate < DateTime.Now.AddYears(-3))
               {
                   var traktSeason = new TraktShowSeasonDto
                   {
                       TraktShowId = await _showService.GetTraktId(episodeDto.ShowId),
                       Season = (uint)episodeDto.SeasonNum
                   };
                   if (traktSeason.TraktShowId != prevShow)
                   {
                       traktShowSeasonList.Add(traktSeason);
                   }

                   prevShow = traktSeason.TraktShowId;
               }
           }
           _logger.LogInformation("TraktService.GetSeasons season episode count is "+ traktShowSeasonList.Count.ToString());
           foreach (var traktShowSeason in traktShowSeasonList)
           {
               _logger.LogInformation("TraktShowId: " +traktShowSeason.TraktShowId);
               var response = await _traktClient.Seasons.GetSeasonAsync(
                   traktShowSeason.TraktShowId, traktShowSeason.Season, extendedInfo);
               
               if (response.IsSuccess)
               {
                   var showDto = await _showService.GetByTraktId(traktShowSeason.TraktShowId);
                   foreach (var traktEpisode in response.Value)
                   {
                       ParseTraktEpisode(traktEpisode, showDto);
                   }
                   await _showService.UpdateAddFromDto(showDto);
               }
               else 
               {
                   _logger.LogError("Unsuccessful response from Trakt");
               }
           }
           _logger.LogInformation("TraktService.GetSeasons finished");
        }
*/
        private async Task AddWatchedListMovie(ITraktMovie traktMovie)
        {
            try
            {
                var movie = ParseTraktMovie(traktMovie);
                await _movieService.UpdateAddFromDto(movie);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        private TraktCollectionMovieDto ParseTraktMovie(ITraktMovie traktMovie)
        {
            var newMovie = new TraktCollectionMovieDto
            {
                Name = traktMovie.Title,
                FirstAiredYear = Convert.ToInt32(traktMovie.Year)
            };

            if (newMovie.TraktCollectionMovieAliasDtos == null)
            {
                newMovie.TraktCollectionMovieAliasDtos = new List<TraktCollectionMovieAliasDto>();
            }
            if (traktMovie.Ids.HasAnyId == true)
            {
                if (traktMovie.Ids.Trakt > 0)
                {
                    var movieAlias = new TraktCollectionMovieAliasDto()
                    {
                        IdType = "trakt",
                        IdValue = traktMovie.Ids.Trakt.ToString()
                    };
                    newMovie.TraktCollectionMovieAliasDtos.Add(movieAlias);
                }
                if (traktMovie.Ids.Imdb.Length > 0)
                {
                    var movieAlias = new TraktCollectionMovieAliasDto
                    {
                        IdType = "imdb",
                        IdValue = traktMovie.Ids.Imdb
                    };
                    newMovie.TraktCollectionMovieAliasDtos.Add(movieAlias);
                }
                if (traktMovie.Ids.Slug.Length > 0)
                {
                    var movieAlias = new TraktCollectionMovieAliasDto
                    {
                        IdType = "slug",
                        IdValue = traktMovie.Ids.Slug
                    };
                    newMovie.TraktCollectionMovieAliasDtos.Add(movieAlias);
                    newMovie.Slug = movieAlias.IdValue;
                }
                if (traktMovie.Ids.Tmdb > 0)
                {
                    var movieAlias = new TraktCollectionMovieAliasDto
                    {
                        IdType = "tmdb",
                        IdValue = traktMovie.Ids.Tmdb.ToString()
                    };
                    
                    newMovie.TraktCollectionMovieAliasDtos.Add(movieAlias);
                }

                return newMovie;
            }
            return null;
        }

        private async Task AddWatchedListShow(ITraktShow traktShow)
        {
            try
            {
                var show = ParseTraktShow(traktShow);
                var show2 = AddShowAlias(show,"Watched List");
                await _showService.UpdateAddFromDto(show2);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("AddWatchedListShow" + ex.Message);
            }
        }

        private CollectionShowDto AddShowAlias(CollectionShowDto show, string watchedList)
        {
            var found = false;
            foreach (var showAlias in show.CollectionShowAliasDtos)
            {
                if ((showAlias.IdType == "List") && (showAlias.IdValue == watchedList))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var showAlias = new CollectionShowAliasDto
                {
                    IdType = "List",
                    IdValue = watchedList
                };

                show.CollectionShowAliasDtos.Add(showAlias);
            }
            return show;
        }
        
        private async Task AddWatchedHistoryShow(ITraktHistoryItem traktHistoryItem)
        {
            if (traktHistoryItem.Episode != null)
            {
                var collectionShow = ParseTraktShow(traktHistoryItem.Show);
                var episodeDto = ParseTraktEpisode(traktHistoryItem.Episode);
                episodeDto.ShowSlug = collectionShow.Slug;
                collectionShow.CollectionEpisodeDtos.Add(episodeDto);
                ParseWatchHistory(collectionShow,traktHistoryItem);
                await _showService.UpdateAddFromDto(collectionShow);
                foreach (var episodeDto2 in collectionShow.CollectionEpisodeDtos)
                {
                    await _episodeService.UpdateAddFromDto(episodeDto2);
                }
            }
            if (traktHistoryItem.Movie != null)
            {
                var collectionMovie = ParseTraktMovie(traktHistoryItem.Movie);
                await _movieService.UpdateAddFromDto(collectionMovie);
            }
        }

        private void ParseWatchHistory(CollectionShowDto show, ITraktHistoryItem traktHistoryItem)
        {
            //_logger.LogInformation("ParseWatchHistory");
            if (traktHistoryItem.Action.DisplayName == "Watch")
            {
                foreach (var episodeDto in show.CollectionEpisodeDtos)
                {
                    var traktEpisodeAlias = new CollectionEpisodeAliasDto();
                    traktEpisodeAlias.IdType = "watched";
                    traktEpisodeAlias.IdValue = traktHistoryItem.WatchedAt.ToString();
                    episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                }
            }
        }

        private CollectionEpisodeDto ParseTraktEpisode(ITraktEpisode traktEpisode)
        {
           // _logger.LogInformation("ParseTraktEpisode");
            var episodeDto = new CollectionEpisodeDto();
            
            if (traktEpisode.Number != null)
            {
                if (traktEpisode.SeasonNumber != null)
                {
                    episodeDto.EpisodeName = "";
                    if (!traktEpisode.Title.IsNullOrEmpty())
                    {
                        episodeDto.EpisodeName = traktEpisode.Title;
                    }
                    episodeDto.AiredDate = Convert.ToDateTime("1999-01-01");

                    if (traktEpisode.FirstAired > episodeDto.AiredDate)
                    {
                        episodeDto.AiredDate = Convert.ToDateTime(traktEpisode.FirstAired);
                    }
                    episodeDto.SeasonNum = (int) traktEpisode.SeasonNumber;
                    episodeDto.EpisodeNum = (int) traktEpisode.Number;
                    if (episodeDto.CollectionEpisodeAliasDtos == null)
                    {
                        episodeDto.CollectionEpisodeAliasDtos = new List<CollectionEpisodeAliasDto>();
                    }
                          
                    if (traktEpisode.Ids.HasAnyId == true)
                    {
                        if (traktEpisode.Ids.Trakt > 0)
                        {
                            var traktEpisodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "trakt",
                                IdValue = traktEpisode.Ids.Trakt.ToString()
                            };
                            episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Imdb != null)
                        {
                            var traktEpisodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "imdb",
                                IdValue = traktEpisode.Ids.Imdb.ToString()
                            };
                            episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Tvdb > 0)
                        {
                            var traktEpisodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "tvdb",
                                IdValue = traktEpisode.Ids.Tvdb.ToString()
                            };
                            episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.Tmdb > 0)
                        {
                            var traktEpisodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "tmdb",
                                IdValue = traktEpisode.Ids.Tmdb.ToString()
                            };
                            episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                        }

                        if (traktEpisode.Ids.TvRage > 0)
                        {
                            var traktEpisodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "TvRage",
                                IdValue = traktEpisode.Ids.TvRage.ToString()
                            };
                            episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                        }
                    }

                    if (traktEpisode.UpdatedAt > DateTime.MinValue )
                    {
                        var traktEpisodeAlias = new CollectionEpisodeAliasDto
                        {
                            IdType = "TraktUpdateAt",
                            IdValue = traktEpisode.UpdatedAt.ToString()
                        };
                        episodeDto.CollectionEpisodeAliasDtos.Add(traktEpisodeAlias);
                    }
                    return episodeDto;
                }
            }
            _logger.LogInformation("Finish ParseTraktEpisode");
            return episodeDto;
        }

        //private
        private  CollectionShowDto ParseTraktShow(ITraktShow traktShow)
        {
        //    _logger.LogInformation("ParseTraktShow");
            var show = new CollectionShowDto
            {
                Name = traktShow.Title,
                FirstAiredYear = (int)traktShow.Year
            };
            if (show.CollectionShowAliasDtos == null)
            {
                show.CollectionShowAliasDtos = new List<CollectionShowAliasDto>();
            }

            if (show.CollectionEpisodeDtos == null)
            {
                show.CollectionEpisodeDtos = new List<CollectionEpisodeDto>();
            }
            if (traktShow.Ids.HasAnyId == true)
            {
                if (traktShow.Ids.Trakt > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "trakt",
                        IdValue = traktShow.Ids.Trakt.ToString()
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                }
                if (traktShow.Ids.Imdb.Length > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "imdb",
                        IdValue = traktShow.Ids.Imdb.ToString()
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                }
                if (traktShow.Ids.Slug.Length > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "slug",
                        IdValue = traktShow.Ids.Slug
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                    show.Slug = seriesAlias.IdValue;
                }
                if (traktShow.Ids.Tvdb > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "tvdb",
                        IdValue = traktShow.Ids.Tvdb.ToString()
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                }
                if (traktShow.Ids.Tmdb > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "tmdb",
                        IdValue = traktShow.Ids.Tmdb.ToString()
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                }
                if (traktShow.Ids.TvRage > 0)
                {
                    var seriesAlias = new CollectionShowAliasDto
                    {
                        IdType = "TvRage",
                        IdValue = traktShow.Ids.TvRage.ToString()
                    };
                    show.CollectionShowAliasDtos.Add(seriesAlias);
                }
            }
            return show;
        }
        
        private CollectionShowDto ParseCalendarShow(ITraktCalendarShow traktShow)
        {
            var collectedShow = new CollectionShowDto();
            collectedShow.Name = traktShow.Title;
            collectedShow.FirstAiredYear = (int)traktShow.Year;
            collectedShow.CollectionShowAliasDtos = new List<CollectionShowAliasDto>();
            collectedShow.CollectionEpisodeDtos = new List<CollectionEpisodeDto>();
            
            if (traktShow.Ids.HasAnyId == true)
            {
                if (traktShow.Ids.Trakt > 0)
                {
                    var showAlias = new CollectionShowAliasDto();
                    showAlias.IdType = "trakt";
                    showAlias.IdValue = traktShow.Ids.Trakt.ToString();
                    collectedShow.CollectionShowAliasDtos.Add(showAlias);
                }
                if (traktShow.Ids.Imdb.Length > 0)
                {
                    var showAlias2 = new CollectionShowAliasDto();
                    showAlias2.IdType = "imdb";
                    showAlias2.IdValue = traktShow.Ids.Imdb;
                    collectedShow.CollectionShowAliasDtos.Add(showAlias2);
                }
                if (traktShow.Ids.Slug.Length > 0)
                {
                    var showAlias3 = new CollectionShowAliasDto();
                    showAlias3.IdType = "slug";
                    showAlias3.IdValue = traktShow.Ids.Slug;
                    collectedShow.Slug = traktShow.Ids.Slug;
                    collectedShow.CollectionShowAliasDtos.Add(showAlias3);
                }
                if (traktShow.Ids.Tvdb > 0)
                {
                    var showAlias4 = new CollectionShowAliasDto();
                    showAlias4.IdType = "tvdb";
                    showAlias4.IdValue = traktShow.Ids.Tvdb.ToString();
                    collectedShow.CollectionShowAliasDtos.Add(showAlias4);
                }
                if (traktShow.Ids.Tmdb > 0)
                {
                    var showAlias5 = new CollectionShowAliasDto();
                    showAlias5.IdType = "tmdb";
                    showAlias5.IdValue = traktShow.Ids.Tmdb.ToString();
                    collectedShow.CollectionShowAliasDtos.Add(showAlias5);
                }
                if (traktShow.Ids.TvRage > 0)
                {
                    var showAlias6 = new CollectionShowAliasDto();
                    showAlias6.IdType = "TvRage";
                    showAlias6.IdValue = traktShow.Ids.TvRage.ToString();
                    collectedShow.CollectionShowAliasDtos.Add(showAlias6);
                }
            }

            if (traktShow.Episode.Number != null)
            {
                if (traktShow.Episode.SeasonNumber != null)
                {
                    var episodeName = "";
                    if (!traktShow.Episode.Title.IsNullOrEmpty())
                    {
                        episodeName = traktShow.Episode.Title;
                    }

                    var collectedEpisode = new CollectionEpisodeDto();
                    collectedEpisode.SeasonNum = (int)traktShow.Episode.SeasonNumber;
                    collectedEpisode.EpisodeNum = (int) traktShow.Episode.Number;
                    collectedEpisode.EpisodeName = episodeName;
                    collectedEpisode.AiredDate =  (DateTime)traktShow.FirstAiredInCalendar;
                    collectedEpisode.CollectionEpisodeAliasDtos = new List<CollectionEpisodeAliasDto>();
                    
                    if (traktShow.Episode.Ids.HasAnyId == true)
                    {
                        if (traktShow.Episode.Ids.Trakt > 0)
                        {
                            var episodeAlias = new CollectionEpisodeAliasDto
                            {
                                IdType = "trakt",
                                IdValue = traktShow.Episode.Ids.Trakt.ToString()
                            };
                            collectedEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias);
                        }

                        if (traktShow.Episode.Ids.Imdb != null)
                        {
                            if (traktShow.Episode.Ids.Imdb.ToString().Length > 0)
                            {
                                var episodeAlias2 = new CollectionEpisodeAliasDto
                                {
                                    IdType = "imdb",
                                    IdValue = traktShow.Episode.Ids.Imdb.ToString()
                                };
                                collectedEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias2);
                            }
                        }
                        if (traktShow.Episode.Ids.Tvdb > 0)
                        {
                            if (traktShow.Episode.Ids.Tvdb.ToString().Length > 0)
                            {
                                var episodeAlias3 = new CollectionEpisodeAliasDto
                                {
                                    IdType = "tvdb",
                                    IdValue = traktShow.Episode.Ids.Tvdb.ToString()
                                };
                                collectedEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias3);
                            }
                         }
                        if (traktShow.Episode.Ids.Tmdb > 0)
                        {
                            if (traktShow.Episode.Ids.Tmdb.ToString().Length > 0)
                            {
                                var episodeAlias4 = new CollectionEpisodeAliasDto
                                {
                                    IdType = "tmdb",
                                    IdValue = traktShow.Episode.Ids.Tmdb.ToString()
                                };
                                collectedEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias4);
                            }
                        }
                        if (traktShow.Episode.Ids.TvRage > 0)
                        {
                            if (traktShow.Episode.Ids.TvRage.ToString().Length > 0)
                            {
                                var episodeAlias5 = new CollectionEpisodeAliasDto
                                {
                                    IdType = "TvRage",
                                    IdValue = traktShow.Episode.Ids.TvRage.ToString()
                                };
                                collectedEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias5);
                            }
                        }
                    }
                    if (traktShow.FirstAiredInCalendar != null)
                    {
                        if (Convert.ToDateTime(collectedEpisode.AiredDate)  != traktShow.FirstAiredInCalendar)
                        {
                            collectedEpisode.AiredDate = (DateTime)traktShow.FirstAiredInCalendar;
                        }
                       
                    }
                    collectedShow.CollectionEpisodeDtos.Add(collectedEpisode);
                }
            }
            return collectedShow;
        }

        private static CollectionShowDto ParseCollectionShow(ITraktCollectionShow traktShow)
        {
            try 
            {
                var collectionShow = new CollectionShowDto();
                collectionShow.Name = traktShow.Title;
                collectionShow.FirstAiredYear = (int)traktShow.Year;
                collectionShow.CollectionShowAliasDtos = new List<CollectionShowAliasDto>();
                collectionShow.CollectionEpisodeDtos = new List<CollectionEpisodeDto>();
                if (traktShow.Ids.HasAnyId == true)
                {
                    if (traktShow.Ids.Trakt > 0)
                    {
                        var seriesAlias = new CollectionShowAliasDto
                        {
                            IdType = "",
                            IdValue = traktShow.Ids.Trakt.ToString()
                        };
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias);
                    }
                    if (traktShow.Ids.Imdb.Length > 0)
                    {
                        var seriesAlias2 = new CollectionShowAliasDto
                        {
                            IdType = "imdb",
                            IdValue = traktShow.Ids.Imdb.ToString()
                        };
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias2);
                    }
                    if (traktShow.Ids.Slug.Length > 0)
                    {
                        var seriesAlias3 = new CollectionShowAliasDto
                        {
                            IdType = "slug",
                            IdValue = traktShow.Ids.Slug
                        };
                        collectionShow.Slug = traktShow.Ids.Slug;
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias3);
                    }
                    if (traktShow.Ids.Tvdb > 0)
                    {
                        var seriesAlias4 = new CollectionShowAliasDto
                        {
                            IdType = "tvdb",
                            IdValue = traktShow.Ids.Tvdb.ToString()
                        };
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias4);
                    }
                    if (traktShow.Ids.Tmdb > 0)
                    {
                        var seriesAlias5 = new CollectionShowAliasDto
                        {
                            IdType = "tmdb",
                            IdValue = traktShow.Ids.Tmdb.ToString()
                        };
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias5);
                    }
                    if (traktShow.Ids.TvRage > 0)
                    {
                        var seriesAlias6 = new CollectionShowAliasDto
                        {
                            IdType = "TvRage",
                            IdValue = traktShow.Ids.TvRage.ToString()
                        };
                        collectionShow.CollectionShowAliasDtos.Add(seriesAlias6);
                    }
                }
                
                if (traktShow.CollectionSeasons.Count() > 0)
                {
                    foreach (var season in  traktShow.CollectionSeasons )
                    {
                        foreach(var episode in season.Episodes)
                        {
                            if (episode.Number != null) 
                            {
                                var newEpisode = new CollectionEpisodeDto();
                                newEpisode.SeasonNum = (int) season.Number;
                                newEpisode.EpisodeNum = (int)episode.Number;
                                newEpisode.AiredDate = Convert.ToDateTime("1999-01-01");
                                newEpisode.CollectionEpisodeAliasDtos  = new List<CollectionEpisodeAliasDto>();
                                
                                var episodeAlias = new CollectionEpisodeAliasDto
                                {
                                    IdType = "collected",
                                    IdValue = traktShow.LastCollectedAt.ToString()
                                };
                                newEpisode.CollectionEpisodeAliasDtos.Add(episodeAlias);
                                collectionShow.CollectionEpisodeDtos.Add(newEpisode);
                            }
                        }
                    }
                }
                return collectionShow;
            }
            catch 
            {
                return null;
            }
        }

        private static TraktCollectionMovieDto ParseCollectionMovie(ITraktCollectionMovie traktMovie)
        {
            if (traktMovie.Year != null)
            {
                var movie = new TraktCollectionMovieDto
                {
                    Name = traktMovie.Title,
                    FirstAiredYear = (int)traktMovie.Year,
                    TraktCollectionMovieAliasDtos = new List<TraktCollectionMovieAliasDto>()
                };
                if (traktMovie.Ids.HasAnyId)
                {
                    if (traktMovie.Ids.Trakt > 0)
                    {
                        var movieAlias = new TraktCollectionMovieAliasDto
                        {
                            IdType = "trakt",
                            IdValue = traktMovie.Ids.Trakt.ToString()
                        };

                        movie.TraktCollectionMovieAliasDtos.Add(movieAlias);
                    }
                    if (traktMovie.Ids.Imdb.Length > 0)
                    {
                        var movieAlias2 = new TraktCollectionMovieAliasDto
                        {
                            IdType = "imdb",
                            IdValue = traktMovie.Ids.Imdb.ToString()
                        };
                        movie.TraktCollectionMovieAliasDtos.Add(movieAlias2);
                    }
                    if (traktMovie.Ids.Slug.Length > 0)
                    {
                        var movieAlias3 = new TraktCollectionMovieAliasDto
                        {
                            IdType = "slug",
                            IdValue = traktMovie.Ids.Slug
                        };
                        movie.TraktCollectionMovieAliasDtos.Add(movieAlias3);
                    }
                    if (traktMovie.Ids.Tmdb > 0)
                    {
                        var movieAlias4 = new TraktCollectionMovieAliasDto
                        {
                            IdType = "tmdb",
                            IdValue = traktMovie.Ids.Tmdb.ToString()
                        };
                        movie.TraktCollectionMovieAliasDtos.Add(movieAlias4);
                    }
                }
                return movie;
            }

            return null;
        }
    }
}
