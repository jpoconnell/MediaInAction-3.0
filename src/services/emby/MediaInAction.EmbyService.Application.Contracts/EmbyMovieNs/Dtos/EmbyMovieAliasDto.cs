using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyMovieNs.Dtos
{
    public class EmbyMovieAliasDto: EntityDto<Guid>
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public EmbyMovieAliasDto Create(string idType, string idValue)
        {
            this.IdType = idType;
            this.IdValue = idValue;
            return this;
        }
    }
}
