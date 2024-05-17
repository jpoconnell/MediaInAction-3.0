using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace  MediaInAction.EmbyService.EmbyEpisodeNs.Dtos;

public class EmbyEpisodeCreateDto
{
    [Required]
    public string ShowName { get; set; }
    [Required]
    public int SeasonNum { get; set; }
    [Required]
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    
    public int EpisodeStatusId { get; set; }
    public string EpisodeName { get; set; }
    public string AltEpisodeId { get; set; }
    public string SeasonEpisode { get; set; }
    public List<EmbyEpisodeAliasCreateDto> EpisodeAliases { get; set; } 
}


