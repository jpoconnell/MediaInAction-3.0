using Volo.Abp.Application.Dtos;

namespace  MediaInAction.VideoService.SeriesNs.Dtos;

public class GetSeriesListInput :  PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}