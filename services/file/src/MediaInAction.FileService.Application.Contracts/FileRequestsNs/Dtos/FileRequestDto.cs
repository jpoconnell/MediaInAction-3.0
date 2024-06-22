using System;
using System.ComponentModel.DataAnnotations;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.FileService.FileRequestsNs.Dtos;

[Serializable]
public class FileRequestDto : CreationAuditedEntityDto<Guid>
{
    [Required]
    
    public string Server { get; set; }
    
    public string FileName { get; set; }

    public bool Directory { get; set; }

    public FileOperation FileOperation { get; set; }

    public string MoveToServer { get; set; }
    
    public string MoveToFileName { get; set; }

    public bool MoveToDirectory { get; set; }
}

