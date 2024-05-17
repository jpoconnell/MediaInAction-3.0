using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.SeriesNs;

public class TraktServiceShowCreatedEventHandler : IDistributedEventHandler<TraktShowCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceShowCreatedEventHandler> _logger;
    private readonly SeriesManager _seriesManager;
    
    public TraktServiceShowCreatedEventHandler(
        IDistributedEventBus eventBus,
        SeriesManager seriesManager,
        ILogger<TraktServiceShowCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _seriesManager = seriesManager;
    }

    public async Task HandleEventAsync(TraktShowCreatedEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
        }

        var tmpName = eventData.Name.ToLower();
        var seriesId = Guid.Empty;
        var acceptedFile = await _seriesManager.AcceptTraktShowAsync(
            traktId.ToString(), eventData.Slug, eventData.Name, eventData.FirstAiredYear,
            eventData.TraktShowCreatedAliases);
        
        if (acceptedFile != null)
        {
            _logger.LogInformation("Sending Trakt Show Accepted Event");
            seriesId = acceptedFile.Id;
            await _eventBus.PublishAsync(new TraktShowAcceptedEto
            {
                TraktId = seriesId.ToString(),
                Slug = eventData.Slug,
                Name = eventData.Name,
                Year = eventData.FirstAiredYear
            });
        }
    }
}