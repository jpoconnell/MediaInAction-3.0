using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.SeriesAliasNs;
[EventName("MediaInAction.SeriesAlias.Created")]
public class SeriesAliasCreatedEto : EtoBase
{
    public Guid Id { get; set; }
    public Guid SeriesAliasId { get; set; }
    public Guid SeriesId { get; set; }
    public string SeriesName { get; set; }
    public int SeriesYear { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
    public bool SeriesIsActive { get;  set; }
}