using System;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.FileRequestNs
{
    [EventName("MediaInAction.FileOperation.Completed")]
    public class FileRequestFailedEto : EtoBase, IHasExtraProperties
    {
        public Guid FileRequestId { get; set; }
        
        public string FailReason { get; set; }
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}