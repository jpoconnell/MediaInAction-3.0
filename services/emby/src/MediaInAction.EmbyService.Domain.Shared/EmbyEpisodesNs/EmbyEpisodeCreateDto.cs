using System;
using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyEpisodeAliasesNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;


public class EmbyEpisodeCreateDto 
{
    public Guid EmbySeriesId  { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public List<EmbyEpisodeAliasCreateDto> EmbyEpisodeAliasCreateDtos { get; set; }
}