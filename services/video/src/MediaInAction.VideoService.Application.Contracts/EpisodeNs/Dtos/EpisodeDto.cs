using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.EpisodeNs.Dtos
{
    public class EpisodeDto : EntityDto<Guid>
    {
        public Guid SeriesId { get; set; }
        public string SeriesName { get; set; }
        public int SeasonNum { get; set; }
        public int EpisodeNum { get; set; }
        public MediaStatus EpisodeStatus { get; set; }
        public DateTime AiredDate { get; set; }
        public string EpisodeName { get; set; }
        public string AltEpisodeId { get; set; }
        public string SeasonEpisode { get; set; }
        public List<EpisodeAliasDto> EpisodeAliasDtos { get; set; }
    }
}
