using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.ShowNs.Dtos
{
    public class CollectionShowDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public string Slug { get; set; }
        public List<CollectionShowAliasDto> CollectionShowAliasDtos { get; set; }
        
        public List<CollectionEpisodeDto> CollectionEpisodeDtos { get; set; }
        
        public CollectionShowDto Create(string name, int firstAiredYear)
        {
            this.Name = name;
            this.FirstAiredYear = firstAiredYear;
            this.CollectionShowAliasDtos = new List<CollectionShowAliasDto>();
            return this;
        }
    }
}