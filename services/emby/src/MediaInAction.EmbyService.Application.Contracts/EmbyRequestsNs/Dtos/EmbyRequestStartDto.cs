using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestsNs.Dtos
{
    [Serializable]
    public class EmbyRequestStartDto
    {
        public Guid EmbyRequestId { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }
    }
}
