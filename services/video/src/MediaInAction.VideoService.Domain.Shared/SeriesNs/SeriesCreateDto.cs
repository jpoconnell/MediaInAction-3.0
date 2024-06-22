using System.Collections.Generic;
using MediaInAction.VideoService.SeriesAliasNs;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string imageName { get; set; }
    public List<SeriesAliasCreateDto> SeriesAliases { get; set; }
}