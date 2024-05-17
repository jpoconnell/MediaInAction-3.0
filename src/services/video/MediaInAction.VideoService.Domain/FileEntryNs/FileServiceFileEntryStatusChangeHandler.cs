using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileServiceFileEntryStatusChangeHandler : IDistributedEventHandler<FileEntryStatusEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<FileServiceFileEntryStatusChangeHandler> _logger;
    private readonly FileEntryManager _fileEntryManager;
    
    public FileServiceFileEntryStatusChangeHandler(
        IDistributedEventBus eventBus,
        FileEntryManager fileEntryManager,
        ILogger<FileServiceFileEntryStatusChangeHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _fileEntryManager = fileEntryManager;
    }

    public async Task HandleEventAsync(FileEntryStatusEto eventData)
    {
        try
        {
            if (!Guid.TryParse(eventData.FileEntryId, out var fileId))
            {
                throw new BusinessException(VideoServiceErrorCodes.FileEntryIdNotGuid);
            }

            var acceptedFile = await _fileEntryManager.FileEntryStatusAsync(
                fileId, eventData.Server, eventData.FileName, eventData.ListName,  
                eventData.FileStatus);

           // _logger.LogInformation("Received FileEntry Status Change Event");
        }
        catch (Exception e)
        {
            _logger.LogDebug("FileServiceFileEntryStatusChangeHandler.HandleEventAsync:" + e.Message);
        }
    }
}