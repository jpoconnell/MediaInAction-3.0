using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.MovieNs.Dtos
{
    public class TraktCollectionMovieDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public string Slug { get; set; }
        public List<TraktCollectionMovieAliasDto> TraktCollectionMovieAliasDtos { get; set; }
        
        
        public TraktCollectionMovieDto Create(string name, int firstAiredYear)
        {
            this.Name = name;
            this.FirstAiredYear = firstAiredYear;
            this.TraktCollectionMovieAliasDtos = new List<TraktCollectionMovieAliasDto>();
            return this;
        }
    }
}