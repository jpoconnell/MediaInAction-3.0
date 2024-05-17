using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktShowNs.Dtos;

public class TraktShowStatusDto : EntityDto
{
    public int CountOfStatusShow { get; set; }
    public string ShowStatus { get; set; }
}