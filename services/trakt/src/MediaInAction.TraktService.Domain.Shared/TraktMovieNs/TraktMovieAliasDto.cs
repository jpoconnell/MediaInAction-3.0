using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieAliasDto: EntityDto<Guid>
    {
        public Guid MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public TraktMovieAliasDto Create(
            Guid movieId,
            string idType, string idValue)
        {
            this.IdType = idType;
            this.IdValue = idValue;
            return this;
        }
    }
}
