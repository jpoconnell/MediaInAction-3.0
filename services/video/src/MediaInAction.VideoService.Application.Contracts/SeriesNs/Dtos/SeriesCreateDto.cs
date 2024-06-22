using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

    public class SeriesCreateDto
    {
        public string Name { get; set; }
        public string Slug  { get; set; }
        public int FirstAiredYear { get; set; }
        
        public MediaType Type { get; set; }
        
        public bool IsActive { get; set; }
        
        public List<SeriesAliasCreateDto> SeriesAliases { get; set; }
        
    }

