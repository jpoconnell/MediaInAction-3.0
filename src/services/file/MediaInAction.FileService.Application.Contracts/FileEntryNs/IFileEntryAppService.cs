using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.FileEntryNs
{
    public interface IFileEntryAppService : IApplicationService
    {
        Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);

        Task<ListResultDto<FileEntryDto>> GetListAsync();

        Task<FileEntryDto> CreateAsync(CreateFileEntryDto input);

        Task<FileEntryDto> UpdateAsync(Guid id, UpdateFileEntryDto input);

        Task DeleteAsync(Guid id);
        
        [ItemNotNull]
        Task<FileEntryDto> GetAsync(Guid fileEntryId);
    }
}