using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.ToBeMappedNs.Dtos;

public class GetToBeMappedsInput :  PagedAndSortedResultRequestDto
{
    public bool Filter { get; set; }
}