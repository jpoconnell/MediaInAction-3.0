using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.DataMaintenanceNs.Dtos;

public class SeriesSeasonDto : EntityDto<Guid>
{
    public Guid SeriesId { get; set; }
    public int Season { get; set; }
}
