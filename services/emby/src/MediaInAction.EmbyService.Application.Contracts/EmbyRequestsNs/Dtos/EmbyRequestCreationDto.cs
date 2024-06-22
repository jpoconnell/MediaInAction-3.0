using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestsNs.Dtos
{
    [Serializable]
    public class EmbyRequestCreationDto
    {
        public string ReferenceId { get; set; }

        [Required]
        // public EmbyOperation Operation { get; set; }

        public List<EmbyRequestItemCreationDto> Embys { get; set; }

    }
}
