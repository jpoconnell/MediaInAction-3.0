using System;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestCompleteInputDto
{
    public string Token { get; set; }
    public int TraktTypeId { get; set; }
}