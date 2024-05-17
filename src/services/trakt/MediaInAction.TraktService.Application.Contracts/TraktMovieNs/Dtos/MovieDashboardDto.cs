using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs.Dtos;

public class MovieDashboardDto: EntityDto
{
    public List<TraktMovieStatusDto> TraktMovieStatusDto { get; set; }
}