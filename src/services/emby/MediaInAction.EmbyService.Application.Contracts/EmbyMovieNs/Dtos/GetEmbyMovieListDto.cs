using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyMovieNs.Dtos
{
    public class GetEmbyMovieListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}