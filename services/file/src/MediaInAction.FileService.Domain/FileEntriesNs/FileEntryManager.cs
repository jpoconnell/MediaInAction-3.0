using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MediaInAction.FileService.FileEntriesNs
{
    public class FileEntryManager : DomainService
    {
        private readonly IRepository<FileEntry, Guid> _fileEntryRepository;
        private readonly ILogger<FileEntryManager> _logger;
        
        public FileEntryManager( 
            IRepository<FileEntry, Guid> fileEntryRepository, 
            ILogger<FileEntryManager> logger)
        {
            _fileEntryRepository = fileEntryRepository;
            _logger = logger;
        }

        public async Task<FileEntry> CreateAsync(FileEntryCreateDto input)
        {
            var existingFileEntry = await _fileEntryRepository.FirstOrDefaultAsync(
                p => p.Server == input.Server &&
                     p.Filename == input.FileName &&
                     p.Directory == input.Directory &&
                     p.Extn == input.Extn);
            if (existingFileEntry != null)
            {
                return null;
            }
            else
            {
                var newFileEntry = await _fileEntryRepository.InsertAsync(
                    new FileEntry(
                        GuidGenerator.Create(),
                        input.Server,
                        input.FileName,
                        input.Directory,
                        input.Directory,
                        input.Size,
                        input.Sequence,
                        ListType.Other,
                        FileStatus.New
                    ));
                return newFileEntry;
            }
        }

        public async Task UpdateFileEntryStatus(Guid id,FileStatus status)
        {
            var fileEntry = await _fileEntryRepository.GetAsync(id);
            if (fileEntry.FileStatus != status)
            {
                fileEntry.FileStatus = status;
                await _fileEntryRepository.UpdateAsync(fileEntry, true);
            }
        }
    }
}