using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyMoviesNs.Dtos
{
    public class EmbyMovieDto : EntityDto<Guid>
    {
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public List<EmbyMovieAliasDto> MovieAliasDtos { get; set; }
        
        public EmbyMovieDto Create(string name, int year)
        {
            this.Name = name;
            this.FirstAiredYear = year;
            return this;
        }
    }
}