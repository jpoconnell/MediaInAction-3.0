using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestNs;

[Serializable]
public class EmbyRequestItemCreationEto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string EmbyName { get; set; }
    
    [Required]
    public int Directory { get; set; }
}
