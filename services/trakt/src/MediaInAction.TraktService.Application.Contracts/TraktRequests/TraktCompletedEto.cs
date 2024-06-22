using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktRequests;

[EventName("MediaInAction.Trakt.Completed")]
public class TraktCompletedEto : EtoBase
{
    public TraktRequestDto TraktRequest { get; set; }
}