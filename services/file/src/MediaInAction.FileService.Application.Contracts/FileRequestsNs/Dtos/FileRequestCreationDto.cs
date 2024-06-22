using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileRequestsNs.Dtos;

    [Serializable]
    public class FileRequestCreationDto
    {
        public string ReferenceId { get; set; }

        [Required]
        public FileOperation Operation { get; set; }

        public List<FileRequestItemCreationDto> Files { get; set; }

    }
