using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyRequestsNs
{
    [EventName("MediaInAction.EmbyOperation.Completed")]
    public class EmbyRequestFailedEto : EtoBase
    {
        public Guid EmbyRequestId { get; set; }
        
        public string FailReason { get; set; }
    
    }
}