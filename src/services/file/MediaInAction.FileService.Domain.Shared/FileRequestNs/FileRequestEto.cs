using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileRequestNs;

[Serializable]
public class FileRequestEto
{
    public Guid ReferenceId { get; set; }
    public string Method { get; set; }

    public FileStatus Status { get; set; }
    public List<FileRequestFileEto> Files { get; set; }

}
