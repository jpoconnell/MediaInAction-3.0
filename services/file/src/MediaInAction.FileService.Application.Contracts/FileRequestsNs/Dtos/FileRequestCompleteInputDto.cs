using System;

namespace MediaInAction.FileService.FileRequestsNs.Dtos;

[Serializable]
public class FileRequestCompleteInputDto
{
    public string Token { get; set; }
    public int TraktTypeId { get; set; }
}