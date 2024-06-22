using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.EmbyRequestsNs;

[Serializable]
public class EmbyRequestEto
{
    public string ReferenceId { get; set; }

    
    [Required]
    public string Server { get; set; }

    [Required]
    public string EmbyName { get; set; }
    
    [Required]
    public string Directory { get; set; }

    public string Command { get; set; }
    public string FromServer { get; set; }
    public string FromDirectory { get; set; }
    public string FromEmbyName { get; set; }
    public string ToServer { get; set; }
    public string ToDirectory { get; set; }
    public string ToEmbyName { get; set; }
}
