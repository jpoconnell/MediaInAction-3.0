using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.DelugeService.RequestNs
{
    [EventName("MediaInAction.Deluge.Completed")]
    public class RequestCompletedEto : EtoBase
    {
        public Guid DelugeRequestId { get; set; }
        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string Currency { get; set; }
        public string BuyerId { get; set; }
        public RequestState State { get; set; }
        public ICollection<RequestProductEto> Products { get; set; } = new List<RequestProductEto>();
    }
}