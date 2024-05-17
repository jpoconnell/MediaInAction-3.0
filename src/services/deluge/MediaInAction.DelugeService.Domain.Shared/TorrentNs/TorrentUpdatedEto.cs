using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.DelugeService.TorrentNs;
[EventName("MediaInAction.Torrent.Updated")]
public class TorrentUpdatedEto : EtoBase
{
    public string Hash { get; set; }
    
}