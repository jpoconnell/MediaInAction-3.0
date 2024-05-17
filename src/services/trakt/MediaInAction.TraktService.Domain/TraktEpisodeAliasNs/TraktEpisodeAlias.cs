using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktEpisodeAliasNs;

public class TraktEpisodeAlias : Entity<Guid>
    {
        public Guid EpisodeId { get; set; }
        public Guid ShowId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktEpisodeAlias() { }
    }
