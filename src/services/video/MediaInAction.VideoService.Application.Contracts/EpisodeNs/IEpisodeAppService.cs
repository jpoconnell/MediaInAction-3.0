using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.EpisodeNs;

public interface IEpisodeAppService : IApplicationService
{
    Task<EpisodeDto> CreateAsync(EpisodeCreateDto input);
    Task<EpisodeDto> GetAsync(Guid id);
    Task<EpisodeDto> GetEpisodeAsync(GetEpisodeInput input);
    Task<List<EpisodeDto>> GetEpisodesAsync(GetEpisodesInput input);
    Task SetAsCompleteAsync(Guid id);
    Task SetAsWatchedAsync(Guid id);
    Task<PagedResultDto<EpisodeDto>> GetListPagedAsync(GetEpisodesInput input);
    Task<DashboardDto> GetDashboardAsync(DashboardInput input);
    Task<EpisodeDto> AcceptTraktEpisodeAsync(EpisodeCreateDto input);
    Task<List<EpisodeDto>> GetMyEpisodesAsync(GetMyEpisodesInput getMyEpisodesInput);
}
