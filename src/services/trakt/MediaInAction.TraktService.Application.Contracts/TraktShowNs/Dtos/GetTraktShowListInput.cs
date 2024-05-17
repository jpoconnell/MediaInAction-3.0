using Volo.Abp.Application.Dtos;

namespace  MediaInAction.VideoService.SeriesNs.Dtos;

public class GetTraktShowListInput :  PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}