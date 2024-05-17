using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

[EventName("MediaInAction.EmbyEpisode.Accepted")]
public class EmbyEpisodeAcceptedEto : EtoBase
{
    public string EmbyId { get; set; }
    public string EmbySeriesId  { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
}