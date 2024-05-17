using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileRequestNs.Dtos;

[Serializable]
public class FileRequestItemCreationDto
{
    public string ReferenceId { get; set; }
    [Required]
    public string FileName { get; set; }
    [Required]
    public string Directory { get; set; }
    [Required]
    public string Server { get; set; }
}
