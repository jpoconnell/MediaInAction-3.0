using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace  MediaInAction.TraktService.MovieNs.Dtos
{
    public class TraktCollectionMovieAliasDto: EntityDto<Guid>
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktCollectionMovieAliasDto()
        {
        }
        
        internal TraktCollectionMovieAliasDto(
            [NotNull] string idType, 
            string idValue)
        {
            this.IdType = idType;
            this.IdValue = idValue;
        }
    }
}
