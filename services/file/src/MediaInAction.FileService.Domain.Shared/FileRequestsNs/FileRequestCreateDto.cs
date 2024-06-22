using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileRequestsNs;


public class FileRequestCreateDto
{
    public Guid ReferenceId { get; set; }
    public FileOperation  Operation  { get; set; }
    public RequestStatus Status { get; set; }
    public List<FileRequestFileCreateDto> Files { get; set; }
}
