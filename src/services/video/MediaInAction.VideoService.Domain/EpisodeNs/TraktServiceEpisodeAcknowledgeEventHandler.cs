using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.EpisodeNs;

public class TraktServiceEpisodeAcknowledgeEventHandler : IDistributedEventHandler<TraktService.TraktEpisodeNs.TraktEpisodeAcknowledgeEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceEpisodeCreatedEventHandler> _logger;
    private readonly EpisodeManager _episodeManager;
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    
    public TraktServiceEpisodeAcknowledgeEventHandler(
        IDistributedEventBus eventBus,
        EpisodeManager episodeManager,
        ISeriesAliasRepository seriesAliasRepository,
        ILogger<TraktServiceEpisodeCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _episodeManager = episodeManager;
        _seriesAliasRepository = seriesAliasRepository;
    }

    public async Task HandleEventAsync(TraktService.TraktEpisodeNs.TraktEpisodeAcknowledgeEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
        }

        var seriesAlias = await _seriesAliasRepository.GetByIdValue(eventData.Slug);
        if (seriesAlias != null)
        {
            var seriesId = seriesAlias.SeriesId;
            var acceptedFile = await _episodeManager.AcceptTraktEpisodeAsync(eventData, seriesId);

            _logger.LogInformation("Received Trakt Episode Accepted Event");
        }
    }
}