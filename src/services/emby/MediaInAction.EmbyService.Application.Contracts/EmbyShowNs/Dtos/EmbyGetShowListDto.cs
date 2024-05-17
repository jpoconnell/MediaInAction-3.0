using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyShowNs.Dtos
{
    public class EmbyGetShowListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}