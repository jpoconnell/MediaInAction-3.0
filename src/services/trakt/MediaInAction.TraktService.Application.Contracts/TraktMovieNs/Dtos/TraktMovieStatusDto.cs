using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs.Dtos;

public class TraktMovieStatusDto : EntityDto
{
    public int CountOfStatusMovie { get; set; }
    public string MovieStatus { get; set; }
}