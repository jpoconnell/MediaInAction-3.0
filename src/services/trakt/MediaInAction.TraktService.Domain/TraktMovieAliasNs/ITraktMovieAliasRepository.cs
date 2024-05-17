using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.TraktService.TraktMovieAliasNs;

public interface ITraktMovieAliasRepository :  IRepository<TraktMovieAlias, Guid>
{
    Task<TraktMovieAlias> FindByTypeValue(string type, string value);

}