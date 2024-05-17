using Volo.Abp.Application.Dtos;

namespace MediaInAction.DelugeService.TorrentNs.Dtos
{
    public class GetTorrentListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}