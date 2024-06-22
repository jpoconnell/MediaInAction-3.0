using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyMoviesNs.Dtos
{
    public class GetEmbyMovieListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}