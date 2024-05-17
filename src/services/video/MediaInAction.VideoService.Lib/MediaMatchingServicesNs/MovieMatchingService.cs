using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MediaInAction.VideoService.MediaMatchingServicesNs;
public class MovieMatchingService : IMovieMatchingService
{
    private readonly IMovieAliasLibService _movieAliasService;
    private readonly IMovieService _movieService;
    private readonly ILogger<MovieMatchingService> _logger;
    
    public MovieMatchingService(
        IMovieAliasLibService movieAliasService,
        IMovieService movieService,
        ILogger<MovieMatchingService> logger)
    {
        _movieAliasService = movieAliasService;
        _movieService = movieService;
        _logger = logger;
    }

    public async Task<bool> GetByMovieName(string alias, List<MovieAliasDto> movieAliasList)
    {
        if (movieAliasList.IsNullOrEmpty())
        {
            return false;
        }
        else
        {
            var myAlias = alias.ToLower();
            bool masterFound = false;
            LevenshteinDistance ld = new LevenshteinDistance();
            string idType = "name";
            int dist = 0;
            int fuzzy = (myAlias.Length / 3) ;
            if (fuzzy < 1)
            {
                fuzzy = 1;
            }

            var tmpAlias = myAlias;
            if (alias.Contains("1080p"))
            {
                tmpAlias = alias.Substring(0, alias.Length - 6);
            }
            if (alias.Contains("720p"))
            {
                tmpAlias = alias.Substring(0, alias.Length - 5);
            }

            myAlias = tmpAlias;
            var movieAliasValue = "";
            try 
            {
                foreach (var movieAlias in movieAliasList)
                {
                    if (myAlias.Contains("afi"))
                    {
                        movieAliasValue = "afi " + movieAlias.IdValue.ToLower();
                    }
                    else 
                    { 
                        movieAliasValue = movieAlias.IdValue.ToLower();
                    }
                    dist = ld.ComputeDistance(myAlias, movieAliasValue);
                    if (dist == 0)
                    {
                        await MovieAliasCreateIfNeeded(movieAlias.MovieId,idType, myAlias);
                        masterFound = true;
                    }
                    if (dist <= fuzzy)
                    {
                        await MovieAliasCreateIfNeeded(movieAlias.MovieId,idType, myAlias);
                        masterFound = true;
                    }

                    if (masterFound == false)
                    {
                        var movieDto = await _movieService.GetByIdAsync(movieAlias.MovieId);
                        
                        movieAliasValue = movieAliasValue + " " + movieDto.FirstAiredYear.ToString();
                        dist = ld.ComputeDistance(tmpAlias.ToLower(), movieAliasValue);
                        if (dist == 0)
                        {
                            await MovieAliasCreateIfNeeded(movieAlias.MovieId,idType, myAlias);
                            masterFound = true;
                        }
                        if (dist <= fuzzy)
                        {
                            await MovieAliasCreateIfNeeded(movieAlias.MovieId,idType, myAlias);
                            masterFound = true;
                        }
                    }
                }
                return masterFound;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("MovieMatchingService.GetByMovieName" + ex.Message);
                return false;
            }
        }
    }

    private async Task MovieAliasCreateIfNeeded(Guid movieId, string idType, string alias)
    {
        try
        {           
            var movieAlias = await _movieAliasService.FindByMovieTypeValueAsync(movieId,idType, alias.ToLower());

            if (movieAlias == null)
            { 
                await _movieAliasService.CreateMovieAlias(movieId,idType, alias.ToLower());
            }   
        }
        catch (Exception ex) 
        {
           _logger.LogDebug("MovieAliasCreateIfNeeded:" +ex.Message);
        } 
    }
}
