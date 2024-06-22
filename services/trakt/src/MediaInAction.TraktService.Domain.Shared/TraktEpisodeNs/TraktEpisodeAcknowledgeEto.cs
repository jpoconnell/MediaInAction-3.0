using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktEpisodeNs;

[EventName("MediaInAction.TraktEpisode.Acknowledge")]
public class TraktEpisodeAcknowledgeEto : EtoBase
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name  { get; set; }
    public int Year { get; set; }
    public int Season { get; set; }
    public int Episode { get; set; }
}