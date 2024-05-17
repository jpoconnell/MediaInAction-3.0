using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.MovieNs;

public class TraktServiceMovieAcknowledgeEventHandler : IDistributedEventHandler<TraktService.TraktMovieNs.TraktMovieAcknowledgeEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceMovieAcknowledgeEventHandler> _logger;
    private readonly MovieManager _movieManager;
    
    public TraktServiceMovieAcknowledgeEventHandler(
        IDistributedEventBus eventBus,
        MovieManager movieManager,
        ILogger<TraktServiceMovieAcknowledgeEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _movieManager = movieManager;
    }

    public async Task HandleEventAsync(TraktService.TraktMovieNs.TraktMovieAcknowledgeEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
        }
        await _movieManager.AckTraktMovieAsync(eventData);
        _logger.LogInformation("Sending Trakt Show Accepted Event");
    }
}