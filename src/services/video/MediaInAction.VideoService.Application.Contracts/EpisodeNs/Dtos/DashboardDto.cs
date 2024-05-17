using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.EpisodeNs.Dtos;
public class DashboardDto: EntityDto
{
    public List<ActiveDto> Actives { get; set; }
    
    public List<EpisodeDto> EpisodeStatusDto { get; set; }
}

