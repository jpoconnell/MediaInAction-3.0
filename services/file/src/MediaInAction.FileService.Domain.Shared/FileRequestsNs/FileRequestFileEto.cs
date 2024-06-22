using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.FileService.FileRequestsNs;

[Serializable]
public class FileRequestFileEto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string FileName { get; set; }
    
    [Required]
    public string Directory { get; set; }
    
    public int Sequence { get; set; }
}
