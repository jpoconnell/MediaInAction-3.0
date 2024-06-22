using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.FileService.FileRequestsNs.Dtos;

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
