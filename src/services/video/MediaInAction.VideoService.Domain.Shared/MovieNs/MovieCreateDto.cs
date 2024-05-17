using System.Collections.Generic;


namespace MediaInAction.VideoService.MovieNs;

public class MovieCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string ImageName { get; set; }
    public List<MovieAliasCreateDto> MovieAliases { get; set; }
}