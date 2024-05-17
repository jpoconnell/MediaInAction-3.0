using System;

namespace MediaInAction.EmbyService.EmbyRequestNs.Dtos;

[Serializable]
public class EmbyRequestCompleteInputDto
{
    public string Token { get; set; }
    public int TraktTypeId { get; set; }
}