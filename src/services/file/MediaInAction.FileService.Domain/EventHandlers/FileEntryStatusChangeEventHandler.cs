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
    public class FileEntryStatusChangeEventHandler : IDistributedEventHandler<FileEntryStatusEto>, ITransientDependency
    {
        private readonly IFileEntryRepository _fileEntryRepository;
        private readonly FileEntryManager _fileEntryManager;
        private readonly ILogger<FileEntryStatusChangeEventHandler> _logger;
        
        public FileEntryStatusChangeEventHandler(IFileEntryRepository fileEntryRepository,
            ILogger<FileEntryStatusChangeEventHandler> logger,
            FileEntryManager fileEntryManager)
        {
            _fileEntryRepository = fileEntryRepository;
            _fileEntryManager = fileEntryManager;
            _logger = logger;
        }

        public async Task HandleEventAsync(FileEntryStatusEto eventData)
        {
            if ((eventData.Directory.IsNullOrEmpty()) || (eventData.FileName.IsNullOrEmpty()) || (eventData.Server.IsNullOrEmpty()))
            {
                _logger.LogDebug("eventData not valid");
            }
            else
            {
                _logger.LogInformation("FileEntry Status Change Event");
                if (!Guid.TryParse(eventData.FileEntryId, out var fileId))
                {
                    // try finding it by 
                   var dbFileEntry = await  _fileEntryRepository.GetByIdentifiers(eventData.Server, 
                       eventData.FileName, eventData.FileName, eventData.ListName);
                   if (dbFileEntry == null)
                   {
                       throw new BusinessException(FileServiceDomainErrorCodes.FileEntryIdNotGuid);
                   }
                   else
                   {
                       await _fileEntryManager.UpdateFileEntryStatus(fileId, FileStatus.Accepted);
                       _logger.LogInformation("FileEntry Status Updated");
                   }
                }
                else
                {
                    try
                    {
                        var dbFileEntry = await _fileEntryRepository.GetByIdentifiers(eventData.Server,
                            eventData.FileName,eventData.Directory,eventData.ListName);
                        if (dbFileEntry == null)
                        {
                            throw new BusinessException(FileServiceDomainErrorCodes.FileEntryIdNotInDatabase);
                        }
                        else
                        {
                            if (dbFileEntry.FileStatus != eventData.FileStatus)
                            {
                                dbFileEntry.FileStatus = eventData.FileStatus;
                                await _fileEntryRepository.UpdateAsync(dbFileEntry, true);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                       _logger.LogDebug(ex.Message);
                    }
                }
            }
        }
    }
}
