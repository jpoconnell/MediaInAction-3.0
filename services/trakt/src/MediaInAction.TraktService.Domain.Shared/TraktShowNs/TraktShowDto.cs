using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public string Slug { get; set; }
        public List<TraktShowAliasDto> TraktShowAliasDtos { get; set; }
        
        public FileStatus TraktStatus { get; set; }
        public TraktShowDto Create(string name, int firstAiredYear)
        {
            this.Name = name;
            this.FirstAiredYear = firstAiredYear;
            this.TraktShowAliasDtos = new List<TraktShowAliasDto>();
            return this;
        }
    }
}