using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public interface ITraktEpisodeAppService : IApplicationService
{
    Task<EpisodeDashboardDto> GetDashboardAsync(EpisodeDashboardInput input);
}