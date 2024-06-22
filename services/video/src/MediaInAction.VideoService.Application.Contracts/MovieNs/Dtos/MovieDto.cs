using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MovieNs.Dtos
{
    public class MovieDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int FirstAiredYear { get; set; }
        public MediaType Type { get; set; }
        public MediaStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<MovieAliasDto> MovieAliasDtos { get; set; }
    }
}