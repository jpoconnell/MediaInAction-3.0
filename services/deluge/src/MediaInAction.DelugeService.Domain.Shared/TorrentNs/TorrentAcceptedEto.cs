using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.DelugeService.TorrentNs;
[EventName("MediaInAction.Torrent.Accepted")]
public class TorrentAcceptedEto : EtoBase
{
    public string Hash { get; set; }
    public string Name { get; set; }
}