using Volo.Abp.Application.Dtos;

namespace  MediaInAction.VideoService.SeriesNs.Dtos;

public class GetSeriesPagedInput :  PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}