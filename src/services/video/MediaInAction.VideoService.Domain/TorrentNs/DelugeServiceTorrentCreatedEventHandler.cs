using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.TorrentNs;

public class DelugeServiceTorrentCreatedEventHandler : IDistributedEventHandler<TorrentCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<DelugeServiceTorrentCreatedEventHandler> _logger;
    private readonly TorrentManager _torrentManager;
    
    public DelugeServiceTorrentCreatedEventHandler(
        IDistributedEventBus eventBus,
        TorrentManager torrentManager,
        ILogger<DelugeServiceTorrentCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _torrentManager = torrentManager;
    }

    public async Task HandleEventAsync(TorrentCreatedEto eventData)
    {
        _logger.LogInformation("Receiving Torrent Created Event");
        var acceptedFile = await _torrentManager.AcceptTorrentAsync(
            eventData.Comment, eventData.IsSeed, eventData.Hash,eventData.Paused,
            eventData.Ratio,eventData.Message, eventData.Name, eventData.Label, 
            eventData.Added, eventData.CompleteTime, eventData.DownloadLocation,
            FileStatus.New, MediaType.Other, Guid.Empty, Guid.Empty);

        _logger.LogInformation("Sending Torrent Accepted Event");
        await _eventBus.PublishAsync(new TorrentAcceptedEto
        {
            Hash = eventData.Hash,
            Name = eventData.Name
        });
    }
}