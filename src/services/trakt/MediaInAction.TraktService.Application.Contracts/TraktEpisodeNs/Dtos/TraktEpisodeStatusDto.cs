using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs.Dtos;

public class TraktEpisodeStatusDto : EntityDto
{
    public int CountOfStatusEpisode { get; set; }
    public string EpisodeStatus { get; set; }
}