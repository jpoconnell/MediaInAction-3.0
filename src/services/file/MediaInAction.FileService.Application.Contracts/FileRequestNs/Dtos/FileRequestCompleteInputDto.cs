using System;

namespace MediaInAction.FileService.FileRequestNs.Dtos;

[Serializable]
public class FileRequestCompleteInputDto
{
    public string Token { get; set; }
    public int TraktTypeId { get; set; }
}