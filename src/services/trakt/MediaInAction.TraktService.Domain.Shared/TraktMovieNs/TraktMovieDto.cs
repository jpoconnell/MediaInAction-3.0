using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieDto : EntityDto<Guid>
    {
        public string Slug { get;  set; }
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        
        public FileStatus MovieStatus { get; set; }
      
        public bool IsActive { get; set; }
       
        public List<( string idType, string idValue)> TraktMovieAliasDtos { get; set; }
        
        public TraktMovieDto Create(string name, int year)
        {
            this.Name = name;
            this.FirstAiredYear = year;
            return this;
        }
    }
}