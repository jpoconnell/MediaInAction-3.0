using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.FileEntryNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.FileService.EventHandlers
{
    public class FileEntryAcceptedEventHandler : IDistributedEventHandler<FileEntryAcceptedEto>, ITransientDependency
    {
        private readonly IFileEntryRepository _fileEntryRepository;
        private readonly FileEntryManager _fileEntryManager;
        private readonly ILogger<FileEntryAcceptedEventHandler> _logger;
        
        public FileEntryAcceptedEventHandler(IFileEntryRepository fileEntryRepository,
            ILogger<FileEntryAcceptedEventHandler> logger,
            FileEntryManager fileEntryManager)
        {
            _fileEntryRepository = fileEntryRepository;
            _fileEntryManager = fileEntryManager;
            _logger = logger;
        }

        public async Task HandleEventAsync(FileEntryAcceptedEto eventData)
        {
            _logger.LogInformation("FileAccepted Event");
            if (!Guid.TryParse(eventData.FileEntryId, out var fileId))
            {
                // try finding it by 
               var dbFileEntry = await  _fileEntryRepository.GetByIdentifiers(eventData.Server, 
                   eventData.FileName, eventData.Directory, eventData.ListName);
               if (dbFileEntry == null)
               {
                   throw new BusinessException(FileServiceDomainErrorCodes.FileEntryIdNotGuid);
               }
               else
               {
                   await _fileEntryManager.UpdateFileEntryStatus(fileId, FileStatus.Accepted);
                   _logger.LogInformation("FileEntry Updated");
               }
            }
            else
            {
                var fileEntry = await _fileEntryRepository.GetAsync(fileId);
                if (fileEntry == null)
                {
                    throw new BusinessException(FileServiceDomainErrorCodes.FileEntryIdNotInDatabase);
                }
                else
                {
                    fileEntry.FileStatus = FileStatus.Accepted;
                    await _fileEntryManager.UpdateFileEntryStatus(fileEntry.Id , fileEntry.FileStatus);
                }
            }
        }
    }
}
