using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.FileService.FileEntryNs;

public class FileEntryDto : AuditedEntityDto<Guid>
{
    public string Server { get; set; }
    public string Directory { get; set; }
    public string Filename { get; set; }
    public string Extn { get; set; }
    public long Size { get; set; }
    public int Sequence { get; set; }
    public ListType ListName { get; set; }
    public FileStatus FileStatus { get; set; }
}
