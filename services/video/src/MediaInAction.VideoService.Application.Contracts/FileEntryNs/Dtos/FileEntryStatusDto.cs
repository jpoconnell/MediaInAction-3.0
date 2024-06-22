using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

public class FileEntryStatusDto : EntityDto
{
    public int CountOfStatusFileEntry { get; set; }
    public string FileEntryStatus { get; set; }
 
}