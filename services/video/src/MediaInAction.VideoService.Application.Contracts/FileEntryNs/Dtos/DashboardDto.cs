using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

public class DashboardDto: EntityDto
{
    public List<FileEntryStatusDto> FileEntryStatusDto { get; set; }
}

