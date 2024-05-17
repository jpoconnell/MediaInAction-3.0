using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs.Dtos;

namespace MediaInAction.VideoService.MovieAliasNs;

public interface IMovieAliasLibService
{
    Task<List<MovieAliasDto>> GetAllByType(string idType);
    Task<MovieAliasDto> FindByMovieTypeValueAsync(Guid movieId, string idType, string alias);
    Task CreateMovieAlias(Guid movieId, string idType, string alias);
    Task<MovieAliasDto> GetByIdValue(string toLower);
}