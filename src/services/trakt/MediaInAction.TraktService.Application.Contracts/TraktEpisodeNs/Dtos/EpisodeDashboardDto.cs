using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs.Dtos;

public class EpisodeDashboardDto: EntityDto
{
    public List<TraktEpisodeStatusDto> TraktEpisodeStatusDto { get; set; }
}