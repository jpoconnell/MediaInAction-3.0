using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService.MovieAliasNs;

public class MovieAlias : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public MovieAlias() { }
        
    }
