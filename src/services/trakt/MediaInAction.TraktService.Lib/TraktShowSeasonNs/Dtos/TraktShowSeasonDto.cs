
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService.TraktShowSeasonNs.Dtos;

public class TraktShowSeasonDto 
{
    public TraktShowDto ShowDto { get; set; }
    public uint Season { get; set; }
}
