using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos;

public class MovieStatusDto : EntityDto
{
    public int CountOfStatusMovie { get; set; }
    public string MovieStatus { get; set; }
 
}