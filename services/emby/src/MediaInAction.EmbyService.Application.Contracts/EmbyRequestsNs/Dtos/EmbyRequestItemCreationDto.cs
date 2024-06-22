using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestsNs.Dtos
{
    [Serializable]
    public class EmbyRequestItemCreationDto
    {
        public string ReferenceId { get; set; }
        [Required]
        public string EmbyName { get; set; }
        [Required]
        public string Directory { get; set; }
        [Required]
        public string Server { get; set; }
    }
}
