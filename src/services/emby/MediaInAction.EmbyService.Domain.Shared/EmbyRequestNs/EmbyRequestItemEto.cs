using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestNs;

[Serializable]
public class EmbyRequestItemEto
{
    public string ReferenceId { get; set; }

    [Required]
    public string Server { get; set; }

    [Required]
    public string EmbyName { get; set; }
    
    [Required]
    public string Directory { get; set; }
}
