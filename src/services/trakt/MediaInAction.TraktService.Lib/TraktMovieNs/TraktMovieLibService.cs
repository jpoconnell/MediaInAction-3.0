using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.MovieNs.Dtos;
using MediaInAction.TraktService.TraktMovieAliasNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieLibService : ITraktMovieLibService
    {
        private readonly ILogger<TraktMovieLibService> _logger;
        private readonly TraktMovieManager _traktMovieManager;
        private readonly ITraktMovieRepository _traktMovieRepository;
        
        public TraktMovieLibService(
            TraktMovieManager traktMovieManager,
            ITraktMovieRepository traktMovieRepository,
            ILogger<TraktMovieLibService> logger
        )
        {
            _traktMovieManager = traktMovieManager;
            _traktMovieRepository = traktMovieRepository;
            _logger = logger;
        }

        public async Task UpdateAddFromDto(TraktCollectionMovieDto movie)
        {
            try 
            { 
                var dbMovieId = await CreateUpdateMovie(movie);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("MovieLibService.UpdateAddFromDto:" + ex.Message);
            }
        }

        public async Task ResendUnAcceptedMoviesList()
        {
            var movies = await _traktMovieRepository.GetListAsync();
            foreach (var movie in movies)
            {
                if (movie.TraktStatus == FileStatus.New)
                {
                    var movieDto = MapToDto(movie);
                    await _traktMovieManager.ResendEvent(movieDto);
                }
            }
        }

        private TraktMovieDto MapToDto(TraktMovie movie)
        {
            var movieDto = new TraktMovieDto();
            movieDto.FirstAiredYear = movie.FirstAiredYear;
            movieDto.Name = movie.Name;
            return movieDto;
        }

        public async Task<List<TraktMovieDto>> GetListAsync()
        {
            var traktMovieListDto = new List<TraktMovieDto>();
            var traktMovieList = await _traktMovieRepository.GetListAsync();
            foreach (var traktMovie in traktMovieList)
            {
                var traktMovieDto = new TraktMovieDto();
                traktMovieDto.FirstAiredYear = traktMovie.FirstAiredYear;
                traktMovieDto.Name = traktMovie.Name;
                traktMovieListDto.Add(traktMovieDto);
            }

            return traktMovieListDto;
        }

        private async Task<Guid> CreateUpdateMovie(TraktCollectionMovieDto traktMovieDto)
        {
            try
            {
                var movieAliases1 = new List< ( string idType, string idValue)>();
               
                var movieAliases2 = new List<TraktMovieAlias>();
                foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
                {
                    movieAliases1.Add((alias.IdType, alias.IdValue));
                    var movieAlias = new TraktMovieAlias();
                    movieAlias.IdValue = alias.IdValue;
                    movieAlias.IdType = alias.IdType;
                    movieAliases2.Add(movieAlias);
                }
                
                var dbMovie = await _traktMovieRepository.GetByMovieNameYearAsync(
                    traktMovieDto.Name, traktMovieDto.FirstAiredYear);
                if (dbMovie == null)
                {
                    var createMovie = new TraktMovieCreateDto();
                    var dbCreatedMovie = await _traktMovieManager.CreateAsync(createMovie);
                    if (dbCreatedMovie != null)
                    {
                        _logger.LogInformation("Trakt Movie Created:" + traktMovieDto.Name + ":" + traktMovieDto.FirstAiredYear.ToString());
                    }
                    return dbCreatedMovie.Id;
                }
                else  //update movie
                {
                    if (traktMovieDto.Slug == null)
                    {
                        traktMovieDto.Slug = GetSlugFromAlias(traktMovieDto);
                    }
                    var returnId = Guid.Empty;
                    var diff = CompareMovie(dbMovie, traktMovieDto);

                    if (diff == true)
                    {
                        dbMovie = UpdateTrakMovie(dbMovie, traktMovieDto);
                    }
                    else
                    {
                        returnId = Guid.Empty;
                    }
                    return returnId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("CreateUpdateMovie" +ex.Message);
                return Guid.Empty;
            }
        }

        private string GetSlugFromAlias(TraktCollectionMovieDto traktMovieDto)
        {
            var result = "";
            foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
            {
                if (alias.IdType == "slug")
                {
                    result = alias.IdValue;
                    break;
                }
            }

            return result;
        }

        private TraktMovie UpdateTrakMovie(TraktMovie dbMovie, 
            TraktCollectionMovieDto traktMovieDto)
        {
            var updatedMovie = dbMovie;

            if (dbMovie.Name != traktMovieDto.Name)
            {
                dbMovie.Name = traktMovieDto.Name;
            }
            
            if (updatedMovie.TraktMovieAliases == null)
            {
                updatedMovie.TraktMovieAliases = new List<(string, string)>();
            }
            else
            {
                foreach (var traktAlias in traktMovieDto.TraktCollectionMovieAliasDtos)
                {
                    updatedMovie.TraktMovieAliases.Add((traktAlias.IdType, traktAlias.IdValue));
                }
            }
            
            return updatedMovie;
        }

        private bool CompareMovie(TraktMovie dbMovie, 
            TraktCollectionMovieDto traktMovieDto)
        {
            var diff = new bool();
            diff = false;
            if (dbMovie.TraktMovieAliases.Count != traktMovieDto.TraktCollectionMovieAliasDtos.Count)
            {
                diff = true;
            }

            if (dbMovie.Slug != traktMovieDto.Slug)
            {
                diff = true;
            }
            
            if (dbMovie.Name != traktMovieDto.Name)
            {
                diff = true;
            }
            if (dbMovie.FirstAiredYear != traktMovieDto.FirstAiredYear)
            {
                diff = true;
            }
            
            foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
            {
                var found = false;
                foreach (var dbAlias in dbMovie.TraktMovieAliases)
                {
                    if (dbAlias.idType == alias.IdType)
                    {
                        found = true;
                    }
                }
                if (found == false)
                {
                    diff = true;
                }
            }
            return diff;
        }
    }
}
