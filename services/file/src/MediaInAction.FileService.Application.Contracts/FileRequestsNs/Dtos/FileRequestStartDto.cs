using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.FileService.FileRequestsNs.Dtos;

    [Serializable]
    public class FileRequestStartDto
    {
        public Guid FileRequestId { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public string CancelUrl { get; set; }
    }
