using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyShowAliasesNs;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public class EmbyShowCreateDto 
{
    public string Id { get; set; }
    public string Server { get; set; }
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    
    public List<EmbyShowAliasCreateDto> EmbyShowAliasesCreateDto { get; set; }
}