using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktShowNs;
public interface ITraktShowAppService : IApplicationService
{
    Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input);
    Task<List<TraktShowDto>> GetTraktShowListAsync(GetTraktShowListInput filter);
    Task<PagedResultDto<TraktShowDto>> GetTraktShowListPagedAsync(GetTraktShowListInput filter);
}
