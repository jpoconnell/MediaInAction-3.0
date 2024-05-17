using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.MovieNs;
[EventName("MediaInAction.Movie.Created")]
public class MovieCreatedEto : EtoBase
{
    public Guid MovieId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public MediaStatus MovieStatus { get; set; }
    public bool IsActive { get; set; }
    public List<MovieAliasCreatedEto> MovieAliases { get; set; }
}