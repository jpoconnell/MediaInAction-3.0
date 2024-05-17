using System;
using System.Collections.Generic;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.SeriesNs;
[EventName("MediaInAction.Series.SetInActive")]
public class SeriesInActiveEto : EtoBase
{
    public Guid SeriesId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<SeriesAliasCreatedEto> SeriesAliases { get; set; }
}