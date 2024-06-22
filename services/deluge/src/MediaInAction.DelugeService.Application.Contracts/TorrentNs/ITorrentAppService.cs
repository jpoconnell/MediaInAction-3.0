using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.DelugeService.TorrentNs
{
    public interface ITorrentAppService : IApplicationService
    {
        Task<PagedResultDto<TorrentDto>> GetTorrentListPagedAsync(GetTorrentListDto filter);
    }
}