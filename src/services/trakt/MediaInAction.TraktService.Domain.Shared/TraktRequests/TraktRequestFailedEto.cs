using System;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktRequests
{
    [EventName("MediaInAction.Trakt.Completed")]
    public class TraktRequestFailedEto : EtoBase, IHasExtraProperties
    {
        public Guid TraktRequestId { get; set; }
        
        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string FailReason { get; set; }
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}