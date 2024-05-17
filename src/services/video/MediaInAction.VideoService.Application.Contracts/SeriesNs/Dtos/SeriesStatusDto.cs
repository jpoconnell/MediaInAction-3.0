using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesStatusDto : EntityDto
{
    public int CountOfStatusSeries { get; set; }
    public string IsActive { get; set; }
 
}