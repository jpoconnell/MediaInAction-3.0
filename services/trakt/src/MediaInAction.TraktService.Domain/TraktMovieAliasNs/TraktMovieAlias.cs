using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktMovieAliasNs;

public class TraktMovieAlias : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktMovieAlias() { }
    }
