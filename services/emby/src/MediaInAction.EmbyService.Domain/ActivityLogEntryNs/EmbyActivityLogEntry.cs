using System;
using MediaInAction.EmbyService.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace  MediaInAction.EmbyService.ActivityLogEntryNs;

public class EmbyActivityLogEntry : AuditedAggregateRoot<Guid>
{
    public long ExternalId { get; set; }
    public string Name { get; set; }
    public string Overview { get; set; }
    public string ShortOverview { get; set; }
    public string Type { get; set; }
    public string ItemId { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string UserId { get; set; }
    public string UserPrimaryImageTag { get; set; }
    public LoggingLogSeverity Severity { get; set; }
    
    public EmbyActivityLogEntry()
    {
    }

    internal EmbyActivityLogEntry(
        Guid id,
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
        : base(id)
    {
        ExternalId = externalId;
        Name = name;
        Type = type;
        Overview = overview;
        ShortOverview = shortOverview;
        ItemId = itemId;
        Date = date;
        UserId = userId;
        UserPrimaryImageTag = userPrimaryImageTag;
        Severity = severity;
    }
}
