using System;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.Lib.FileServicesNs.Dtos;

[Serializable]
public class MediaLocationDto 
{
    public ListType ListName { get; set; }
    public string Directory { get; set; }
}

