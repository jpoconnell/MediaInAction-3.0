using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktShowAliasNs;

public class TraktShowAlias : Entity<Guid>
    {
        public Guid ShowId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktShowAlias() { }
        
    }
