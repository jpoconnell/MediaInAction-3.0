using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyRequestsNs
{
    [EventName("MediaInAction.EmbyOperation.Completed")]
    public class EmbyRequestCompletedEto : EtoBase
    {
        public Guid EmbyRequestId { get; set; }
        //public EmbyOperation Operation { get; set; }
        public EmbyRequestState State { get; set; }
        public ICollection<EmbyRequestItemEto> Embys { get; set; } = new List<EmbyRequestItemEto>();
        
    }
}