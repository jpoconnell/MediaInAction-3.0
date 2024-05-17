using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos;

public class DashboardDto: EntityDto
{
    public List<MovieStatusDto> MovieStatusDto { get; set; }
}

