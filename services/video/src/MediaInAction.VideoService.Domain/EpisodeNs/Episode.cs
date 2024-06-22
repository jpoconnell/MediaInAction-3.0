using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.EpisodeNs;

public class Episode : AuditedAggregateRoot<Guid>
{
    [Required]
    public Guid SeriesId { get; set; }
    [Required]
    public int SeasonNum { get; set; }
    [Required]
    public int EpisodeNum { get; set; }
    public MediaStatus MediaStatus { get; set; }
    public FileStatus EventStatus { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public string AltEpisodeId { get; set; }
    public string SeasonEpisode { get; set; }
    public string Source { get; set; }
    public List<EpisodeAlias> EpisodeAliases { get;  set; }
    
    private Episode()
    {
    }

    internal Episode(Guid id, Guid seriesId, int seasonNum, int episodeNum, 
        DateTime airedDate,
        string episodeName = "", string source = "",
        string altEpisodeId = "", string seasonEpisode = "") :
        base(id)
    {
        SeriesId = seriesId;
        SeasonNum = seasonNum;
        EpisodeNum = episodeNum;
        AiredDate = airedDate;
        EpisodeName = episodeName;
        Source = source;
        AltEpisodeId = altEpisodeId;
        SeasonEpisode = seasonEpisode;
        MediaStatus = SetEpisodeStatus(MediaStatus.New);
        EpisodeAliases = new List<EpisodeAlias>();
    }
    
    internal Episode(Guid id, Guid seriesId, int seasonNum, int episodeNum):
        base(id)
    {
        SeriesId = seriesId;
        SeasonNum = seasonNum;
        EpisodeNum = episodeNum;
        MediaStatus = SetEpisodeStatus(MediaStatus.New);
    }

    public Episode AddEpisodeAlias(Guid id, Guid episodeId, string idType, string idValue)
    {
        var existingAliasForEpisode = EpisodeAliases.SingleOrDefault(o => o.EpisodeId == episodeId &&
            o.IdType == idType &&
            o.IdValue == idValue);

        if (existingAliasForEpisode != null)
        {

        }
        else
        {
            var episodeAlias = new EpisodeAlias(id, episodeId, idType, idValue);
            EpisodeAliases.Add(episodeAlias);
        }

        return this;
    }
    
    public MediaStatus SetEpisodeStatus(MediaStatus status)
    {
        MediaStatus = status;
        return MediaStatus;
    }
}
