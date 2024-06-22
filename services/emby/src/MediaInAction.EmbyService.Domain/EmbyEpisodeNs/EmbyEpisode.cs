using System;
using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyEpisodeAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace  MediaInAction.EmbyService.EmbyEpisodeNs;

public class EmbyEpisode : AuditedAggregateRoot<Guid>
{
    public Guid ShowId { get; set; }
    public string SeasonId  { get; set; }
    public DateTime AddedAt { get; set; }
    public string TvDbId { get; set; }
    public string ImdbId { get; set; }
    public string TheMovieDbId { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate  { get; set; }
    public long Size  { get; set; }
    public int SeasonEpisodeNum { get; set; }
    
    public List<EmbyEpisodeAlias> EpisodeAliases { get; set; }
    
    public EmbyEpisode()
    {}

    public void SetId(Guid create)
    {
        Id = create;
    }
}
