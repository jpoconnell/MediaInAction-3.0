using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.MovieNs;
[EventName("MediaInAction.MovieAlias.Created")]
public class MovieAliasCreatedEto : EtoBase
{
    public Guid Id { get; set; }
    public Guid MovieAliasId { get; set; }
    public Guid MovieId { get; set; }
    public string MovieName { get; set; }
    public int MovieYear  { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
    public bool MovieIsActive { get; set; }
    public MediaStatus MovieStatus { get; set; }
    
}