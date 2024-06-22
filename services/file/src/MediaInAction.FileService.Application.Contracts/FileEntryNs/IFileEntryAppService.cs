using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.FileService.FileEntriesNs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.FileEntryNs
{
    public interface IFileEntryAppService : IApplicationService
    {
        Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);

        Task<ListResultDto<FileEntryDto>> GetListAsync();

        Task<FileEntryDto> CreateAsync(FileEntryCreateDto input);

        Task<FileEntryDto> UpdateAsync(Guid id, FileEntryDto input);

        Task DeleteAsync(Guid id);
        
        [ItemNotNull]
        Task<FileEntryDto> GetAsync(Guid fileEntryId);
    }
}