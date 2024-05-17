using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

[EventName("MediaInAction.EmbyEpisode.Created")]
public class EmbyEpisodeCreatedEto : EtoBase
{
    public string EmbyId { get; set; }
    public string EmbySeriesId  { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public List<EmbyEpisodeAliasCreatedEto> Aliases { get; set; }
}