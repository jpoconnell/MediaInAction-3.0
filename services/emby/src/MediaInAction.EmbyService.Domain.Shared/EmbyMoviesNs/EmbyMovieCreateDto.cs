using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyMovieAliasesNs;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public class EmbyMovieCreateDto 
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    
    public List<EmbyMovieAliasCreateDto> EmbyMovieAliasCreateDtos { get; set; }
    
}