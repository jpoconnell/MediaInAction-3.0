using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.FileService.FileEntryNs
{
    [Authorize(FileServicePermissions.FileEntry.Default)]
    public class FileEntryAppService : ApplicationService, IFileEntryAppService
    {
        private readonly FileEntryManager _fileEntryManager;
        private readonly IRepository<FileEntry, Guid> _fileEntryRepository;

        public FileEntryAppService(FileEntryManager fileentryManager, IRepository<FileEntry, Guid> fileentryRepository)
        {
            _fileEntryManager = fileentryManager;
            _fileEntryRepository = fileentryRepository;
        }
        
        [AllowAnonymous]
        public async Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
        {
            var fileEntryList = await _fileEntryRepository.GetListAsync();

            var totalCount = fileEntryList.Count;

            return new PagedResultDto<FileEntryDto>(
                totalCount,
                ObjectMapper.Map<List<FileEntry>, List<FileEntryDto>>(fileEntryList)
            );
        }
        
        [AllowAnonymous]
        public async Task<ListResultDto<FileEntryDto>> GetListAsync()
        {
            var fileentries = await _fileEntryRepository.GetListAsync();
            return new ListResultDto<FileEntryDto>(
                ObjectMapper.Map<List<FileEntry>, List<FileEntryDto>>(fileentries)
            );
        }

        
        [AllowAnonymous]
        public async Task<FileEntryDto> GetAsync(Guid id)
        {
            var fileentry = await _fileEntryRepository.GetAsync(id);
            return ObjectMapper.Map<FileEntry, FileEntryDto>(fileentry);
        }
        
        [Authorize(FileServicePermissions.FileEntry.Create)]
        public async Task<FileEntryDto> CreateAsync(FileEntryCreateDto input)
        {
            var fileEntry = await _fileEntryManager.CreateAsync(input);
            return ObjectMapper.Map<FileEntry, FileEntryDto>(fileEntry);
        }

        [Authorize(FileServicePermissions.FileEntry.Update)]
        public async Task<FileEntryDto> UpdateAsync(Guid id, FileEntryDto input)
        {
            var fileentry = await _fileEntryRepository.GetAsync(id);

            fileentry.SetName(input.Filename);
            

            await _fileEntryRepository.UpdateAsync(fileentry);

            return ObjectMapper.Map<FileEntry, FileEntryDto>(fileentry);
        }

        [Authorize(FileServicePermissions.FileEntry.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _fileEntryRepository.DeleteAsync(id);
        }
    }
}