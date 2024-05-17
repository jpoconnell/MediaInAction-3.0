using System.Collections.Generic;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class EmbyShowDto 
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public string Server { get; set; }
    public int Year { get; set; }
    
    public List<EmbyShowAliasDto> ShowAliases { get; set; }
}
