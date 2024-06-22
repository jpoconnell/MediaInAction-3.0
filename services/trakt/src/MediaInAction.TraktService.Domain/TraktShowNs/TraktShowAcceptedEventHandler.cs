using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowAcceptedEventHandler : IDistributedEventHandler<TraktShowAcceptedEto>, ITransientDependency
{
    private readonly ITraktShowRepository _showRepository;
    private readonly TraktShowManager _showManager;
    private readonly ILogger<TraktShowAcceptedEventHandler> _logger;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public TraktShowAcceptedEventHandler(ITraktShowRepository showRepository,
        ILogger<TraktShowAcceptedEventHandler> logger,
        TraktShowManager showManager,
        IDistributedEventBus distributedEventBus)
    {
        _showRepository = showRepository;
        _showManager = showManager;
        _logger = logger;
        _distributedEventBus = distributedEventBus;
    }

    public async Task HandleEventAsync(TraktShowAcceptedEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            // try finding it by 
           var dbTraktShow = await _showRepository.GetBySlug(eventData.Slug);
           if (dbTraktShow == null)
           {
               throw new BusinessException(TraktServiceDomainErrorCodes.TraktShowNotInDatabase);
           }
           else
           {
               var updatedTraktShow = new TraktShowDto();
               updatedTraktShow.Name = dbTraktShow.Name;
               updatedTraktShow.Slug = dbTraktShow.Slug;
               updatedTraktShow.FirstAiredYear = dbTraktShow.FirstAiredYear;
               await _showManager.UpdateTraktShowStatusAsync(updatedTraktShow, FileStatus.Accepted);
               _logger.LogInformation("TraktShow Updated");
           }
        }
        else
        {
            var traktShow = await _showRepository.GetBySlug(eventData.Slug);
            if (traktShow == null)
            {
                throw new BusinessException(TraktServiceDomainErrorCodes.TraktShowIdNotInDatabase);
            }
            else
            {
                if (traktShow.IsActive != true)
                {
                    traktShow.IsActive = true;
                    await _showRepository.UpdateAsync(traktShow, true);
                    // send event back to video service
                    await PublishTraktShowAcknowledgeEvent(eventData);
                }
            }
        }
    }
    
    private async Task PublishTraktShowAcknowledgeEvent(TraktShowAcceptedEto eventData)
    {
        await _distributedEventBus.PublishAsync(new TraktShowAcknowledgeEto
        {
            TraktId = eventData.TraktId,
            Slug = eventData.Slug,
            Name = eventData.Name,
            Year = eventData.Year
        });
    }
}
