using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyShowAliasNs;

public interface IEmbyShowAliasRepository :  IRepository<EmbyShowAlias, Guid>
{
    Task<EmbyShowAlias> FindByTypeValue(string showId);

}