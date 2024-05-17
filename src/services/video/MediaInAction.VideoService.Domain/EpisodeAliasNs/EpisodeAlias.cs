using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.VideoService.EpisodeAliasNs;

public class EpisodeAlias: Entity<Guid>
{
    [Required]
    public Guid EpisodeId {get; set; }
    [Required]
    public string IdType { get; set; }
    [Required]
    public string IdValue { get; set; }
    
    public EpisodeAlias() { }
    

    public EpisodeAlias(Guid id, Guid episodeId, [NotNull]string idType, [NotNull]string idValue )
        : base(id)
    {
        EpisodeId = episodeId;
        IdType = Check.NotNullOrEmpty(idType, nameof(idType));
        IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
    }
    
    public EpisodeAlias( [NotNull]string idType, [NotNull]string idValue )
    {
        IdType = Check.NotNullOrEmpty(idType, nameof(idType));
        IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
    }
}

