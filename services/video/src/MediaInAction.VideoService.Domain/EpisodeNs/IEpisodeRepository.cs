using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs;

public interface IEpisodeRepository : IRepository<Episode, Guid>
{
    Task<List<Episode>> GetEpisodesByUserId(
        Guid userId,
        ISpecification<Episode> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<Episode>> GetListPagedAsync(
        ISpecification<Episode> spec,
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
    
    Task<Episode> FindBySeriesIdSeasonEpisodeNum(
        Guid seriesId,
        int seasonNum,
        int episodeNum,
        bool includeDetails = true);
    
    Task<List<Episode>> GetListAsync(
        ISpecification<Episode> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<Episode>> GetDashboardAsync(
        ISpecification<Episode> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<Episode> GetBySlugSeasonEpisode(string slug, int season, int episode);
    Task<Episode> GetByIdAsync(Guid episodeId);
    Task<List<EpisodeAlias>> GetBySlug(string requestSlug);
}
