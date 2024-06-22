using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.TorrentNs;
public interface ITorrentAppService : IApplicationService
{
    Task<TorrentDto> GetAsync(Guid id);
    Task<TorrentDto> CreateAsync(TorrentCreatedDto input);
    Task<TorrentDto> GetTorrentAsync(GetTorrentInput input);
    Task<List<TorrentDto>> GetTorrentsAsync(GetTorrentsInput input);
    Task<PagedResultDto<TorrentDto>> GetListPagedAsync(GetTorrentsInput input);
}
