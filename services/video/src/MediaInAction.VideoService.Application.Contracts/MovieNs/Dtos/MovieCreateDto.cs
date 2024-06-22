using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.MovieAliasNs.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos;
public class MovieCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public MediaStatus MovieStatus { get; set; }
    public bool IsActive { get; set; }
    public string ImageName  { get; set; }
    public List<MovieAliasCreateDto> MovieAliases { get; set; }
    public string Slug { get; set; }
}
