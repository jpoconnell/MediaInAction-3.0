using System;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.DelugeService.RequestNs
{
    [EventName("MediaInAction.Deluge.Completed")]
    public class RequestFailedEto : EtoBase
    {
        public Guid DelugeRequestId { get; set; }
        
        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string FailReason { get; set; }
        
    }
}