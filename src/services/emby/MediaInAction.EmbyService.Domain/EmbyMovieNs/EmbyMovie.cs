using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class EmbyMovie:  AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string ServerId { get; set; }
    public int ProductionYear { get; set; }
    public long Size  { get; set; }
    public object Type { get; set; }
    public object MediaStreams { get; set; }
    public int FirstAiredYear { get; set; }
}
