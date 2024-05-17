using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktMovieNs;

public interface ITraktMovieAppService : IApplicationService
{
    Task<MovieDashboardDto> GetDashboardAsync(MovieDashboardInput input);
}
