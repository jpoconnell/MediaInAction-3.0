using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyShowNs.Dtos
{
    public class EmbyShowCreateDto
    {
        [Required]
        public string ShowSlug { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        
        public List<EmbyShowAliasCreateDto> ShowAliasCreateDtos { get; set; } 
    }
}

