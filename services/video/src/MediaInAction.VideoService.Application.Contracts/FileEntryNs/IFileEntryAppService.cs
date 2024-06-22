using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.FileEntryNs;

public interface IFileEntryAppService : IApplicationService
{
    Task<FileEntryDto> GetAsync(Guid id);
    Task<FileEntryDto> CreateAsync(FileEntryCreatedDto input);
    Task<FileEntryDto> GetFileEntryAsync(GetFileEntryInput input);
    Task<List<FileEntryDto>> GetFileEntriesAsync(GetFileEntriesInput input);
    Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(GetFileEntriesInput input);
    Task<DashboardDto> GetDashboardAsync(DashboardInput input);
    Task SetAsMappedAsync(Guid id);
}
