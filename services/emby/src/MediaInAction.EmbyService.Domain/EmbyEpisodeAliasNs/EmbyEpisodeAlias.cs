using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService.EmbyEpisodeAliasNs;

public class EmbyEpisodeAlias : Entity<Guid>
    {
        public Guid EpisodeId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public EmbyEpisodeAlias() { }
        
    }
