using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.FileRequestNs.Dtos;

    [EventName("MediaInAction.FileRequest.Completed")]
    public class FileCompletedEto : EtoBase
    {
        public FileRequestDto FileRequest { get; set; }
    }