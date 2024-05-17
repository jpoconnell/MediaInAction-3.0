using System;
using System.Collections.Generic;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktRequests
{
    [EventName("MediaInAction.Trakt.Completed")]
    public class TraktRequestCompletedEto : EtoBase, IHasExtraProperties
    {
        public Guid TraktRequestId { get; set; }
        public string OrderId { get; set; }
        public int OrderNo { get; set; }
        public string Currency { get; set; }
        public string BuyerId { get; set; }
        public TraktRequestState State { get; set; }
        public ICollection<TraktRequestProductEto> Products { get; set; } = new List<TraktRequestProductEto>();
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}