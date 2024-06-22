using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.EpisodeNs.Dtos;

public class EpisodeStatusDto : EntityDto
{
    public int CountOfStatusEpisode { get; set; }
    public string EpisodeStatus { get; set; }
}