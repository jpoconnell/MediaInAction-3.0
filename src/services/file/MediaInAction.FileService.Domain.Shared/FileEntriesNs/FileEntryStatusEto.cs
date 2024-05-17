using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.FileEntriesNs;

[EventName("MediaInAction.FileEntry.Status")]
public class FileEntryStatusEto : EtoBase
{
    public string FileEntryId { get; set; }
    public string Server { get; set; }
    public string FileName { get; set; }
    
    public string Directory { get; set; }
    public ListType ListName  { get; set; }
    
    public FileStatus FileStatus  { get; set; }
}