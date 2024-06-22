using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.EpisodeNs.Dtos;

public class GetEpisodesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}