using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowAliasesNs.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyShowAliasesNs;

public interface IEmbyShowAliasRepository :  IRepository<EmbyShowAlias, Guid>
{
    Task<EmbyShowAlias> FindByTypeValue(string showId);

}