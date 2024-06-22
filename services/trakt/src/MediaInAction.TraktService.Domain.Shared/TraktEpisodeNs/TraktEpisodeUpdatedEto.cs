using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktEpisodeNs;

[EventName("MediaInAction.TraktEpisode.Updated")]
public class TraktEpisodeUpdatedEto : EtoBase
{
    public string ShowSlug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName  { get; set; }
    
    public List<( string idType, string idValue)> TraktUpdatedEtoEpisodeAliases { get; set; }
 
}