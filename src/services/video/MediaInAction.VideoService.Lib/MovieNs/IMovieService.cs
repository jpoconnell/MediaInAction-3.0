using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using MediaInAction.VideoService.MovieNs.Dtos;

namespace MediaInAction.VideoService.MovieNs;

public interface IMovieService
{
    Task<MovieDto> GetByIdAsync(Guid id);
    Task<MovieDto> GetByName(string movieName);
    
    Task UpdateAsync(MovieDto movieDto);
}