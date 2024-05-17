using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.SeriesNs;
[EventName("MediaInAction.Series.Created")]
public class SeriesCreatedEto : EtoBase
{
    public string SourceId { get; set; }
    public Guid SeriesId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    
    public MediaType Type { get; set; }
    public List<SeriesAliasCreatedEto> SeriesAliases { get; set; }
}