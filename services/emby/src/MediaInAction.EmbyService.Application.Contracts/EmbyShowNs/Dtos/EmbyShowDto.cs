using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyShowNs.Dtos
{
    public class EmbyShowDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public string Slug { get; set; }
        public List<EmbyShowAliasDto> ShowAliasDtos { get; set; }
        
        public EmbyShowDto Create(string name, int firstAiredYear)
        {
            this.Name = name;
            this.FirstAiredYear = firstAiredYear;
            this.ShowAliasDtos = new List<EmbyShowAliasDto>();
            return this;
        }
    }
}