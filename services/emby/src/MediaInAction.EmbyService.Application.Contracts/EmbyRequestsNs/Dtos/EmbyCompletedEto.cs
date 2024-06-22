using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyRequestsNs.Dtos
{
    [EventName("MediaInAction.EmbyRequest.Completed")]
    public class EmbyCompletedEto : EtoBase
    {
        public EmbyRequestDto EmbyRequest { get; set; }
    }
}