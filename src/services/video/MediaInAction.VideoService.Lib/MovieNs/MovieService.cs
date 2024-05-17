using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.MovieNs;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<MovieService> _logger;
    
    public MovieService(
        IMovieRepository movieRepository,
        ILogger<MovieService> logger)
    {
        _movieRepository = movieRepository;
        _logger = logger;
    }

    public async Task<MovieDto> GetByIdAsync(Guid link)
    {
        var movie = await _movieRepository.GetAsync(link);
        return MapToMovieDto(movie);
    }

    private MovieDto MapToMovieDto(Movie movie)
    {
        var movieDto = new MovieDto
        {
            Name = movie.Name,
            FirstAiredYear = movie.FirstAiredYear,
            MovieStatus = movie.MovieStatus,
            Id = movie.Id
        };
        return movieDto;
    }

    public async Task<MovieDto> GetByName(string movieName)
    {
       var movie = await _movieRepository.GetByMovieNameAsync(movieName);
       if (movie != null)
       {
           return MapToMovieDto(movie);
       }
       else
       {
           return null;
       }
    }
    
    public async Task UpdateAsync(MovieDto movieDto)
    {
        var movie = await _movieRepository.FindByMovieNameYear(movieDto.Name,
            movieDto.FirstAiredYear);

        if (movie.MovieStatus != movieDto.MovieStatus)
        {
            movie.MovieStatus = movieDto.MovieStatus;
            await _movieRepository.UpdateAsync(movie, true);
        }
    }
}