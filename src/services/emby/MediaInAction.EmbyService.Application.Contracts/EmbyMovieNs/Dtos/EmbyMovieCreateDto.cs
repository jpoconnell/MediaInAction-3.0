using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyMovieNs.Dtos
{
    public class EmbyMovieCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        public string Slug { get; set; }
        public DateTime ReleaseDate { get; set; }
        
        public List<EmbyMovieAliasCreateDto> MovieAliases { get; set; } 
    }
}

