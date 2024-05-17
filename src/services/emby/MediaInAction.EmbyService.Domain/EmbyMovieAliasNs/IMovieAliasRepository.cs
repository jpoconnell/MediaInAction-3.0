using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.MovieAliasNs;

public interface IMovieAliasRepository :  IRepository<MovieAlias, Guid>
{
    Task<MovieAlias> FindByTypeValue(string showId);

}