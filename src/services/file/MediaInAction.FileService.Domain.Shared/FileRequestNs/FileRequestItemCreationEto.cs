using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.FileService.FileRequestNs;

[Serializable]
public class FileRequestItemCreationEto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string FileName { get; set; }
    
    [Required]
    public int Directory { get; set; }
}
