using System.Collections.Generic;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowCreateDto 
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<( string idType, string idValue)> TraktShowCreatedAliases { get; set; }
}