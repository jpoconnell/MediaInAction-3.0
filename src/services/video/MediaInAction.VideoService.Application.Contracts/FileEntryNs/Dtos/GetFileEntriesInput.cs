using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

public class GetFileEntriesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}