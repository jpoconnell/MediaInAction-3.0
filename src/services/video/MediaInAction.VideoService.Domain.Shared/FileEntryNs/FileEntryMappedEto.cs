using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.FileEntryNs;

[EventName("MediaInAction.FileEntry.Cancelled")]
public class FileEntryMappedEto : EtoBase
{
    public Guid PaymentRequestId { get; set; }
    public Guid FileEntryId { get; set; }
    public int FileEntryNo { get; set; }
    public DateTime FileEntryDate { get; set; }

}