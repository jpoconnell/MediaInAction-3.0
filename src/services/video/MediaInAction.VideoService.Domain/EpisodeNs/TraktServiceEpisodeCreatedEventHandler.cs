using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.VideoService.SeriesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.EpisodeNs;

public class TraktServiceEpisodeCreatedEventHandler : IDistributedEventHandler<TraktService.TraktEpisodeNs.TraktEpisodeCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceEpisodeCreatedEventHandler> _logger;
    private readonly EpisodeManager _episodeManager;
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    public TraktServiceEpisodeCreatedEventHandler(
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

    public async Task HandleEventAsync(TraktService.TraktEpisodeNs.TraktEpisodeCreatedEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
        }

        if ((eventData.SeasonNum > 0) && (eventData.EpisodeNum > 0))
        {
            var seriesAlias = await _seriesAliasRepository.GetByIdValue(eventData.ShowSlug);
            if (seriesAlias != null)
            {
                var seriesId = seriesAlias.SeriesId;
                var acceptEpisode = await _episodeManager.AcceptTraktEpisodeAsync(eventData, seriesId);

                _logger.LogInformation("Sending Trakt Episode Accepted Event");
                await _eventBus.PublishAsync(new TraktEpisodeAcceptedEto
                {
                    TraktId = acceptEpisode.Id.ToString(),
                    Slug = eventData.ShowSlug,
                    Season = eventData.SeasonNum,
                    Episode = eventData.EpisodeNum
                });
            }
        }
        else
        {
            if (eventData.EpisodeNum == 0)
            {
                throw new BusinessException(VideoServiceErrorCodes.TraktEpisodeSeasonNumZero);
            }
            if (eventData.EpisodeNum == 0)
            {
                throw new BusinessException(VideoServiceErrorCodes.TraktEpisodeEpisodeNumZero);
            }

        }
    }
}