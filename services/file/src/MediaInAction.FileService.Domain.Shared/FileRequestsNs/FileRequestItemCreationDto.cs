using System.ComponentModel.DataAnnotations;

namespace MediaInAction.FileService.FileRequestsNs;


public class FileRequestItemCreationDto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string FileName { get; set; }
    
    [Required]
    public int Directory { get; set; }
}
