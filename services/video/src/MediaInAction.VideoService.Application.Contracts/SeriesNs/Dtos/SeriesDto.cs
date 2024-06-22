using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
    public List<SeriesAliasDto> SeriesAliasDtos { get; set; }
}
