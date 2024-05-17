using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyEpisodeAliasNs;

public interface IEmbyEpisodeAliasRepository :  IRepository<EmbyEpisodeAlias, Guid>
{
    Task<EmbyEpisodeAlias> FindByTypeValue(string showId);

}