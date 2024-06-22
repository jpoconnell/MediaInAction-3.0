using System;
using System.Collections;
using System.Collections.Generic;
using MediaInAction.VideoService.EpisodeAliasNs;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeCreateDto 
{
    public string SeriesName { get; set; }
    public string SeriesYear { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime? AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public List<( string idType, string idValue)> EpisodeCreateAliases { get; set; }

}


