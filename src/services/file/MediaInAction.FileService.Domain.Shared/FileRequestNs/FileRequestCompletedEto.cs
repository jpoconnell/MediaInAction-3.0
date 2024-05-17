using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.FileRequestNs
{
    [EventName("MediaInAction.FileOperation.Completed")]
    public class FileRequestCompletedEto : EtoBase, IHasExtraProperties
    {
        public Guid FileRequestId { get; set; }
        public FileOperation Operation { get; set; }
        public FileRequestState State { get; set; }
        
        public ICollection<FileRequestFileEto> Files { get; set; } = new List<FileRequestFileEto>();
        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}