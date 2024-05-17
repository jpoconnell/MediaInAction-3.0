using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktShowNs.Dtos;

public class TraktShowDashboardDto: EntityDto
{
    public List<TraktShowStatusDto> TraktShowStatusDto { get; set; }
  
}