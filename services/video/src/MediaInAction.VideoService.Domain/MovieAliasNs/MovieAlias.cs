using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.VideoService.MovieAliasNs;

public class MovieAlias: Entity<Guid>
{
    public Guid MovieId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
    
    public MovieAlias() { }
    
    public MovieAlias(Guid id, Guid movieId, [NotNull]string idType, [NotNull]string idValue )
        : base(id)
    {
        MovieId = movieId;
        IdType = Check.NotNullOrEmpty(idType, nameof(idType));
        IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
    }

    public MovieAlias(Guid movieId, [NotNull]string idType, [NotNull]string idValue )
    {
        Id = Guid.NewGuid();
        MovieId = movieId;
        IdType = Check.NotNullOrEmpty(idType, nameof(idType));
        IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
    }
}
