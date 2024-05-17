using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace  MediaInAction.EmbyService.EmbyItems;

public class EmbyItemManager : DomainService
{
    private readonly IEmbyItemRepository _embyItemRepository;
    private ILogger<EmbyItemManager> _logger;
    
    public EmbyItemManager(
        IEmbyItemRepository embyItemRepository,
        ILogger<EmbyItemManager> logger
    )
    {
        _embyItemRepository = embyItemRepository;
        _logger = logger;
    }

    public EmbyItem Items { get; set; }


    public async Task<EmbyItem> CreateAsync(
        string externalId,
        string serverId,
        string type,
        int level,
        string parentId,
        long? runTimeTicks,
        string mediaType)
    {
        var newEmbyItem = new EmbyItem(
        
            id: GuidGenerator.Create(),
            externalId: externalId,
            serverId: serverId,
            type:  type,
            level:  level,
            runTimeTicks:  runTimeTicks,
            parentId: parentId,
            mediaType:  mediaType
        );
        
        try
        {
            var dbActivityLogEntry = await _embyItemRepository.FindByExternalId(newEmbyItem.EmbyItemId);

            if (dbActivityLogEntry == null)
            {
                var createActivityLogEntry = await _embyItemRepository.InsertAsync(newEmbyItem, true);
                return createActivityLogEntry;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
        
    }
}
