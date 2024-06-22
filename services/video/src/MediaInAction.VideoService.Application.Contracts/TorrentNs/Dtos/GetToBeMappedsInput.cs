using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.TorrentNs.Dtos;

public class GetTorrentsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    
}