using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileServiceFileEntryCreatedEventHandler : IDistributedEventHandler<FileEntryCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<FileServiceFileEntryCreatedEventHandler> _logger;
    private readonly FileEntryManager _fileEntryManager;
    
    public FileServiceFileEntryCreatedEventHandler(
        IDistributedEventBus eventBus,
        FileEntryManager fileEntryManager,
        ILogger<FileServiceFileEntryCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _fileEntryManager = fileEntryManager;
    }

    public async Task HandleEventAsync(FileEntryCreatedEto eventData)
    {
        if (!Guid.TryParse(eventData.FileEntryId, out var fileId))
        {
            throw new BusinessException(VideoServiceErrorCodes.FileEntryIdNotGuid);
        }

        var acceptedFile = await _fileEntryManager.AcceptFileEntryAsync(
            fileId, eventData.Server, eventData.Directory,eventData.Extn,
            eventData.Size,eventData.FileName, eventData.ListName, eventData.Sequence, 
            FileStatus.New);

        _logger.LogInformation("Sending FileEntry Accepted Event");
        await _eventBus.PublishAsync(new FileEntryAcceptedEto
        {
            FileEntryId = eventData.FileEntryId,
            Server = eventData.Server,
            FileName = eventData.FileName,
            ListName = eventData.ListName,
            Directory = eventData.Directory
        });
    }
}