using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace  MediaInAction.VideoService.MovieAliasNs;

public interface IMovieAliasRepository : IRepository<MovieAlias, Guid>
{
    Task<MovieAlias> FindByMovieTypeValueAsync(Guid movieId, string idType, string idValue);
    Task<List<MovieAlias>> GetByIdType(string idType);
    Task<MovieAlias> GetByIdValue(string idValue);
}
