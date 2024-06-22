using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class DashboardDto: EntityDto
{
    public List<ActiveDto> Actives { get; set; }
    public List<SeriesStatusDto> SeriesStatusDto { get; set; }
}

