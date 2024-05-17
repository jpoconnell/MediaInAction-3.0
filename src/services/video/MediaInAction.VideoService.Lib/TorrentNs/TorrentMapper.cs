using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MediaMatchingServicesNs;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.TorrentNs;

public class TorrentMapper : ITorrentMapper
{
    private readonly ILogger<TorrentMapper> _logger;
    private readonly ITorrentService _torrentEntryService;
    private readonly IParserService _parserService;
    private readonly IEpisodeService _episodeService;
    private readonly ISeriesAliasService _seriesAliasService;
    private readonly IMovieAliasLibService _movieAliasService;
    private readonly IMovieService _movieService;
    private readonly IToBeMappedService _toBeMappedService;
    private readonly ISeriesMatchingService _seriesMatchingService;
    private readonly IMovieMatchingService _movieMatchingService;
    
    public TorrentMapper(
        ILogger<TorrentMapper> logger,
        ITorrentService torrentEntryService,
        IParserService parserService,
        IEpisodeService episodeService,
        ISeriesAliasService seriesAliasService,
        IMovieService movieService,
        IMovieAliasLibService movieAliasService,
        IToBeMappedService toBeMappedService,
        ISeriesMatchingService seriesMatchingService,
        IMovieMatchingService movieMatchingService
        )
    {
        _logger = logger;
        _torrentEntryService = torrentEntryService;
        _parserService = parserService;
        _episodeService = episodeService;
        _seriesAliasService = seriesAliasService;
        _movieAliasService = movieAliasService;
        _toBeMappedService = toBeMappedService;
        _movieService = movieService;
        _seriesMatchingService = seriesMatchingService;
        _movieMatchingService = movieMatchingService;
    }

    public async Task MapTorrents()
    {
        _logger.LogInformation("Running MapTorrents Service");
        var returnList = new List<TorrentDto>();
       
        var torrentDtoList = await _torrentEntryService.GetUnMapped();
        var startCnt = torrentDtoList.Count;
        var cnt = 0;
        _logger.LogInformation("Total Torrent Entries:" + startCnt.ToString());
        foreach (var torrentDto in torrentDtoList)
        {
            torrentDto.Updates = 0; 
            if ((torrentDto.IsMapped == false) && (torrentDto.Name.Length > 1))
            {
                try
                {
                    var parser = await _parserService.MapProcess(torrentDto);
                    if (parser.MediaType == MediaType.Other)
                    {
                        await _parserService.GetMediaType(parser);
                    }

                    if (parser.MediaType == MediaType.Movie)
                    {
                        await this.MapTorrentsToMovies(torrentDto, parser);
                    }

                    if (parser.MediaType == MediaType.Episode)
                    {
                        await this.MapTorrentsToEpisodes(torrentDto, parser);
                    }

                    if (parser.MediaType == MediaType.Other)
                    {
                        if (parser.ToBeMapped == true)
                        {
                            //await _toBeMappedService.CreateToBeMappedASync(parser.SeriesName);
                        }
                    }
                    if ((torrentDto.EpisodeLink != Guid.Empty) && (torrentDto.IsMapped == false))
                    {
                        torrentDto.IsMapped = true;
                        torrentDto.Updates++;
                    }
                    cnt++;
                    if (cnt > 300)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("Already"))
                    {
                        _logger.LogDebug("TorrentMapper.MapTorrents:" + ex.Message);
                    }
                }
            }
        }
        foreach (var torrentEntryDto in torrentDtoList)
        {
            if (torrentEntryDto.Updates > 0)
            {
                returnList.Add(torrentEntryDto);
            }
        }

        if (returnList.Count > 0)
        {
            await UpdateTorrents(returnList);
        }
        _logger.LogInformation("TorrentEntries Not Mapped:" + startCnt.ToString());
        _logger.LogInformation("TorrentEntries To Be Updated:" + returnList.Count.ToString());
    }
    
    private async Task<bool> MapTorrentsToEpisodes(TorrentDto torrentDto, ParserDto parser)
    {
        torrentDto.Type = MediaType.Episode;
        
        if ((torrentDto.MediaLink == Guid.Empty) && (parser.SeriesLink != Guid.Empty))
        {
            torrentDto.MediaLink = parser.SeriesLink;
            torrentDto.Updates++;
        }

        if ((torrentDto.EpisodeLink == Guid.Empty) && (parser.Link != Guid.Empty))
        {
            torrentDto.EpisodeLink = parser.Link;
            torrentDto.Updates++;
        }
        
        if (torrentDto.EpisodeLink != Guid.Empty)
        {
            var episodeDto = await _episodeService.GetByIdAsync(torrentDto.EpisodeLink );
            
            if ((episodeDto.EpisodeStatus == MediaStatus.New)
                || (episodeDto.EpisodeStatus == MediaStatus.Indexed))
            {
                episodeDto.EpisodeStatus = MediaStatus.Torrent;
                await _episodeService.UpdateAsync(episodeDto);
            }
        }
        else 
        {
            if (parser.SeriesLink != Guid.Empty)
            {
                await _episodeService.CreateAsync(parser);
            }
            if (parser.ToBeMapped == true)
            {
                if (parser.SeriesName.Length > 1)
                {
                    await _toBeMappedService.CreateToBeMappedASync(parser.SeriesName);
                }
            }
            return true;
        }
        return true;
    }

    private async Task<bool> MapTorrentsToMovies(TorrentDto torrentDto, ParserDto parser)
    {
        var result = false;
        try 
        {
            if ((torrentDto.MediaLink == Guid.Empty) && (parser.Link != Guid.Empty))
            {
                torrentDto.MediaLink = parser.Link;
                torrentDto.Updates++;
            }
        
            if (torrentDto.MediaLink != Guid.Empty)
            {
                var movieDto = await _movieService.GetByIdAsync(torrentDto.MediaLink );
                
                if ((movieDto.MovieStatus == MediaStatus.New)
                    || (movieDto.MovieStatus == MediaStatus.Indexed)
                    || (movieDto.MovieStatus == MediaStatus.Compressed))
                {
                    movieDto.MovieStatus = MediaStatus.Compressed;
                    await _movieService.UpdateAsync(movieDto);
                }
            }
            else
            {
                if (parser.ToBeMapped == true)
                {
                    if (parser.SeriesName.Length > 1)
                    {
                        await _toBeMappedService.CreateToBeMappedASync(parser.SeriesName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if(!ex.Message.Contains("Already"))
            {
                _logger.LogDebug("MapTorrentsToMovies:" + ex.Message);
            }
        }         
        return result;
    }
    
    public async Task ProcessToBeMapped()
    {
        _logger.LogInformation("Starting ProcessToBeMapped Service");
        try
        {
            var toBeMappedList = await _toBeMappedService.GetNotProcessed();
            _logger.LogInformation("ToBeMapped Count:" + toBeMappedList.Count.ToString());
            var seriesAliasList = await _seriesAliasService.GetAllByType("name");
            var movieAliasList = await _movieAliasService.GetAllByType("name");
            
            foreach (var toBeMapped in toBeMappedList)
            {
                try
                {
                    if (toBeMapped.Alias.Length > 0)
                    {
                       var found = await _seriesMatchingService.GetBySeriesName(toBeMapped.Alias, seriesAliasList);
                       if (found == false)
                       {
                           var found2 = await _movieMatchingService.GetByMovieName(toBeMapped.Alias, movieAliasList);
                       }
                    }

                    toBeMapped.Processed = true;
                    await _toBeMappedService.UpdateAsync(toBeMapped);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("ProcessToBeMapped:" + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
        _logger.LogInformation("ProcessToBeMapped finished");
    }
    
    private async Task UpdateTorrents(List<TorrentDto> torrentList)
    {
        _logger.LogInformation("Running UpdateTorrents");
        foreach (var torrentEntryDto in torrentList)
        {
            if ((torrentEntryDto.EpisodeLink != Guid.Empty) && (torrentEntryDto.IsMapped == true))
            {
                torrentEntryDto.IsMapped = true;
                await _torrentEntryService.UpdateAsync(torrentEntryDto);
            }
        }
    }
}
