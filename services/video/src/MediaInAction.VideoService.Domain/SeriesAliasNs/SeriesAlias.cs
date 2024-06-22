using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.VideoService.SeriesAliasNs
{
    public class SeriesAlias : Entity<Guid>
    {
        public Guid SeriesId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public SeriesAlias() { }
        
        public SeriesAlias(Guid id, Guid seriesId, [NotNull]string idType, [NotNull]string idValue )
            : base(id)
        {
            SeriesId = seriesId;
            IdType = Check.NotNullOrEmpty(idType, nameof(idType));
            IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
        }

        public SeriesAlias(Guid seriesId, [NotNull]string idType, [NotNull]string idValue )
        {
            Id = Guid.NewGuid();
            SeriesId = seriesId;
            IdType = Check.NotNullOrEmpty(idType, nameof(idType));
            IdValue = Check.NotNullOrEmpty(idValue, nameof(idValue));
        }
    }
}
