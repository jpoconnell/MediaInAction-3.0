using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyRequestNs.Dtos;

[Serializable]
public class EmbyRequestDto : CreationAuditedEntityDto<Guid>
{
    [Required]
    
    public string Server { get; set; }
    
    public string EmbyName { get; set; }

    public bool Directory { get; set; }

    //public EmbyOperation EmbyOperation { get; set; }

    public string MoveToServer { get; set; }
    
    public string MoveToEmbyName { get; set; }

    public bool MoveToDirectory { get; set; }
}

