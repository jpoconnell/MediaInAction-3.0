using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace  MediaInAction.VideoService.EpisodeAliasNs;

public interface IEpisodeAliasRepository : IRepository<EpisodeAlias, Guid>
{

    Task<EpisodeAlias> GetByIdValue(string idValue);
    Task<EpisodeAlias> FindByTypeValue(string embyid, string eventDataEmbyId);
    Task<EpisodeAlias> FindByEpisodeIdType(Guid episodeId, string idType);
    Task<EpisodeAlias> FindByEpisodeValueAsync(
        Guid episodeId, string idValue);
    Task<EpisodeAlias> GetByEpisodeTypeValue(Guid episodeId, 
        string idType, string idValue);
}
