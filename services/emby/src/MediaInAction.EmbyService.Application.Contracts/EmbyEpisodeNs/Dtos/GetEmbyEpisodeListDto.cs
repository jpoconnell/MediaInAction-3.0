using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyEpisodeNs.Dtos
{
    public class GetEmbyEpisodeListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
