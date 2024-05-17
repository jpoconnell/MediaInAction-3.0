using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace  MediaInAction.TraktService.ShowNs.Dtos
{
    public class CollectionShowAliasDto: EntityDto<Guid>
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public CollectionShowAliasDto()
        {
        }
        
        internal CollectionShowAliasDto(
            [NotNull] string idType, 
            string idValue)
        {
            this.IdType = idType;
            this.IdValue = idValue;
        }
    }
}
