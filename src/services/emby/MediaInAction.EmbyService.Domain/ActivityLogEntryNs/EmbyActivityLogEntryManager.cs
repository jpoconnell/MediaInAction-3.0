using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.ActivityLogEntryNs;

public class EmbyActivityLogEntryManager : DomainService
{
    private readonly IEmbyActivityLogEntryRepository _activityLogEntryRepository;
    private ILogger<EmbyActivityLogEntryManager> _logger;
    
    public EmbyActivityLogEntryManager(
        ILogger<EmbyActivityLogEntryManager> logger,
        IEmbyActivityLogEntryRepository activityLogEntryRepository )
    {
        _logger = logger;
        _activityLogEntryRepository = activityLogEntryRepository;
    }
    public async Task<EmbyActivityLogEntry> CreateAsync(
        long externalId,
        string name,
        string overview,
        string shortOverview,
        string type,
        string itemId,
        DateTimeOffset date,
        string userId,
        string userPrimaryImageTag, 
        LoggingLogSeverity severity 
    )
    {
        var activityLogEntry = new EmbyActivityLogEntry
        (
            id: GuidGenerator.Create(),
            externalId:  externalId,
            name:  name,
            overview:  overview,
            shortOverview:  shortOverview,
            type:  type,
            itemId:  itemId,
            date:  date,
            userId:  userId,
            userPrimaryImageTag:  userPrimaryImageTag, 
            severity:  severity
        );

        try
        {
            var dbActivityLogEntry = await _activityLogEntryRepository.FindByExternalId(activityLogEntry.ExternalId);

            if (dbActivityLogEntry == null)
            {
                var createActivityLogEntry = await _activityLogEntryRepository.InsertAsync(activityLogEntry, true);
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
