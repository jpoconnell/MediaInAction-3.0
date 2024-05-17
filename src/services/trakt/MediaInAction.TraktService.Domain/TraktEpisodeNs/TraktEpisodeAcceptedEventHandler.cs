using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeAcceptedEventHandler : IDistributedEventHandler<TraktEpisodeAcceptedEto>, ITransientDependency
{
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;
    private readonly ILogger<TraktEpisodeAcceptedEventHandler> _logger;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public TraktEpisodeAcceptedEventHandler(ITraktEpisodeRepository traktEpisodeRepository,
        ILogger<TraktEpisodeAcceptedEventHandler> logger,
        TraktEpisodeManager traktEpisodeManager,
        IDistributedEventBus distributedEventBus)
    {
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        _logger = logger;
        _distributedEventBus = distributedEventBus;
    }

    public async Task HandleEventAsync(TraktEpisodeAcceptedEto eventData)
    {
        //_logger.LogInformation("Got TraktEpisodeAcceptedEto Event");
        
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            // try finding it by 
           var dbTraktEpisode = await _traktEpisodeRepository.GetByIdentifier(
               eventData.Slug,eventData.Season,eventData.Episode);
           if (dbTraktEpisode == null)
           {
               throw new BusinessException(TraktServiceDomainErrorCodes.TraktEpisodeNotInDatabase);
           }
           else
           {
               var updatedTraktEpisode = await _traktEpisodeManager.UpdateTraktStatus(traktId, FileStatus.Accepted);
               if (updatedTraktEpisode != null)
               {
                   await PublishTraktEpisodeAcknowledgeEvent(eventData);
                   _logger.LogInformation("TraktEpisode Updated");
               }
           }
        }
        else
        {
            if ((eventData.Episode > 0) && (eventData.Season > 0) && (eventData.Slug.Length > 0))
            {
                var traktEpisode = await _traktEpisodeRepository.GetByIdentifier(
                    eventData.Slug,eventData.Season,eventData.Episode);
                if (traktEpisode == null)
                {
                    throw new BusinessException(TraktServiceDomainErrorCodes.TraktEpisodeIdNotInDatabase);
                }
                else
                {
                    traktEpisode.TraktStatus = FileStatus.Accepted;
                    await _traktEpisodeRepository.UpdateAsync(traktEpisode, true);
                    await PublishTraktEpisodeAcknowledgeEvent(eventData);
                }
            }
            else
            {
                _logger.LogInformation("Bad Passed Data");
            }
        }
    }

    private async Task PublishTraktEpisodeAcknowledgeEvent(TraktEpisodeAcceptedEto eventData)
    {
        await _distributedEventBus.PublishAsync(new TraktEpisodeAcknowledgeEto
        {
            TraktId = eventData.TraktId,
            Slug = eventData.Slug ,
            Season = eventData.Season,
            Episode = eventData.Episode,
             
        });
    }
}
