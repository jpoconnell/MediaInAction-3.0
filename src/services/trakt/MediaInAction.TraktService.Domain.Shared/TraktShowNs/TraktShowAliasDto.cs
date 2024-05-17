using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace  MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowAliasDto: EntityDto<Guid>
    {
        public Guid ShowId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktShowAliasDto()
        {
        }
        
        internal TraktShowAliasDto(
            Guid showId,
            [NotNull] string idType, 
            string idValue)
        {
            this.ShowId = showId;
            this.IdType = idType;
            this.IdValue = idValue;
        }
    }
}
