using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesIsActiveDto : EntityDto
{
    public int CountOfActiveSeries { get; set; }
    public int TotalCountOfActiveSeries { get; set; }
}