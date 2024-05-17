using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.MovieAliasNs;

public class MovieAliasLibService: IMovieAliasLibService
{
    private readonly IMovieAliasRepository _movieAliasRepository;
    private readonly ILogger<MovieAliasLibService> _logger;
    
    public MovieAliasLibService(
        IMovieAliasRepository movieAliasRepository,
        ILogger<MovieAliasLibService> logger)
    {
        _movieAliasRepository = movieAliasRepository;
        _logger = logger;
    }
    
    public async Task<List<MovieAliasDto>> GetAllByType(string idType)
    {
        var movieAliases = await _movieAliasRepository.GetByIdType(idType);
        if (movieAliases.Count > 0)
        {
            return MapToMovieAliasDtos(movieAliases);
        }
        else
        {
            return null;
        }
    }
    
    public async Task<MovieAliasDto> FindByMovieTypeValueAsync(
        Guid movieId, 
        string idType, 
        string alias)
    {
        var movieAlias = await _movieAliasRepository.FindByMovieTypeValueAsync(movieId, idType, alias);
        if (movieAlias == null)
        {
            return null;
        }
        else
        {
            return MapToMovieAliasDto(movieAlias);
        }
    }
    
    public async Task CreateMovieAlias(Guid movieId, 
        string idType, 
        string alias)
    {
        try
        {
            var movieAlias = new MovieAlias
            {
                MovieId = movieId,
                IdType = idType,
                IdValue = alias
            };

            var dbMovieAlias = await _movieAliasRepository.FindByMovieTypeValueAsync(movieId, idType, alias.ToLower());
            if (dbMovieAlias == null)
            {
                await _movieAliasRepository.InsertAsync(movieAlias,true);
            }
            else  // update Movie Alias
            {
                
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("MovieAliasLibService.CreateMovieAlias:" +ex.Message);
        }
    }

    public async Task<MovieAliasDto> GetByIdValue(string idValue)
    {
        try
        {
            var movieAlias = await _movieAliasRepository.GetByIdValue(idValue);
            if (movieAlias != null)
            {
                return MapToMovieAliasDto(movieAlias);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private List<MovieAliasDto> MapToMovieAliasDtos(List<MovieAlias> movieAliases)
    {
        var movieAliasDtos = new List<MovieAliasDto>();
        foreach (var movieAlias in movieAliases)
        {
            movieAliasDtos.Add(  MapToMovieAliasDto(movieAlias));
        }

        return movieAliasDtos;
    }

    private MovieAliasDto MapToMovieAliasDto(MovieAlias movieAlias)
    {
        var movieAliasDto = new MovieAliasDto
        {
            IdType = movieAlias.IdType,
            IdValue = movieAlias.IdValue,
            MovieId = movieAlias.MovieId,
        };
        return movieAliasDto;
    }
}

