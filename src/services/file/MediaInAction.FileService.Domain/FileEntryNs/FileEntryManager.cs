using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.FileService.FileEntryNs
{
    public class FileEntryManager : DomainService
    {
        private readonly IRepository<FileEntry, Guid> _fileEntryRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ILogger<FileEntryManager> _logger;
        
        public FileEntryManager( IDistributedEventBus distributedEventBus,
            IRepository<FileEntry, Guid> fileEntryRepository, 
            ILogger<FileEntryManager> logger)
        {
            _fileEntryRepository = fileEntryRepository;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }

        public async Task<FileEntry> CreateAsync(
            [NotNull] string server,
            [NotNull] string filename,
            string directory,
            string extn,
            long size,
            int sequence,
            ListType listname = ListType.Compressed,
            FileStatus status = FileStatus.New)
        {
            var existingFileEntry = await _fileEntryRepository.FirstOrDefaultAsync(
                p => p.Server == server &&
                     p.Filename == filename &&
                     p.Directory == directory &&
                     p.Extn == extn);
            if (existingFileEntry != null)
            {
                // Send update event if 
                if (existingFileEntry.FileStatus == FileStatus.New)
                {
                    await SendCreateFileEntryEvent(existingFileEntry);
                }
                return null;
            }
            else
            {
                var newFileEntry = await _fileEntryRepository.InsertAsync(
                    new FileEntry(
                        GuidGenerator.Create(),
                        server,
                        filename,
                        directory,
                        extn,
                        size,
                        sequence,
                        listname,
                        status
                    ));

                await SendCreateFileEntryEvent(newFileEntry);
                return newFileEntry;
            }
        }

        private async Task SendCreateFileEntryEvent(FileEntry newFileEntry)
        {
            // Publish FileEntry created event
            _logger.LogInformation("Sending FileEntry Creation Event: " + newFileEntry.Filename);
            await _distributedEventBus.PublishAsync(new FileEntryCreatedEto
            {
                FileEntryId = newFileEntry.Id.ToString(),
                Server = newFileEntry.Server,
                FileName = newFileEntry.Filename,
                Directory = newFileEntry.Directory,
                Extn = newFileEntry.Extn,
                Size = newFileEntry.Size,
                ListName = newFileEntry.ListName,
                FileStatus = newFileEntry.FileStatus,
                Sequence = newFileEntry.Sequence
            });
        }

        public async Task UpdateFileEntryStatus(Guid id,FileStatus status)
        {
            var fileEntry = await _fileEntryRepository.GetAsync(id);
            if (fileEntry.FileStatus != status)
            {
                fileEntry.FileStatus = status;
                await _fileEntryRepository.UpdateAsync(fileEntry, true);
                await SendFileEntryStatusChangeEvent(fileEntry);
            }
        }

        private async Task SendFileEntryStatusChangeEvent(FileEntry fileEntry)
        {
            await _distributedEventBus.PublishAsync(new FileEntryStatusEto()
            {
                FileEntryId = fileEntry.Id.ToString(),
                Server = fileEntry.Server,
                Directory = fileEntry.Directory,
                FileName = fileEntry.Filename,
                ListName = fileEntry.ListName,
                FileStatus = fileEntry.FileStatus
            });
            _logger.LogInformation("Sent FileEntry Status Change Event: " + fileEntry.Filename);
        }

        public async Task ResendEvent(FileEntry fileEntry)
        {
            await SendCreateFileEntryEvent(fileEntry);
        }
    }
}