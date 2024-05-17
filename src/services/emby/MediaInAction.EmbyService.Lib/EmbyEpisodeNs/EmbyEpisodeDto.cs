using System;
using System.Collections.Generic;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class EmbyEpisodeDto 
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public string ShowId { get; set; }
    public string ShowName { get; set; }
    public string EmbySeriesId { get; set; }
    public string EmbySeasonId { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    
    public List<EmbyEpisodeAliasDto> EpisodeAliases { get; set; }
}
