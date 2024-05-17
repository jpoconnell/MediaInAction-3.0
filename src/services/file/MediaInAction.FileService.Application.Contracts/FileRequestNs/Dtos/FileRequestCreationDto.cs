using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileRequestNs.Dtos;

    [Serializable]
    public class FileRequestCreationDto
    {
        public string ReferenceId { get; set; }

        [Required]
        public FileOperation Operation { get; set; }

        public List<FileRequestItemCreationDto> Files { get; set; }

    }
