using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.TraktService.TraktShowAliasNs;

public interface ITraktShowAliasRepository :  IRepository<TraktShowAlias, Guid>
{
    Task<TraktShowAlias> FindByTypeValue(string type, string value);
    Task<TraktShowAlias> ListByType(string type);
    Task<TraktShowAlias> ListByValue(string value);
    Task<TraktShowAlias> ListByShow(Guid showId);

}