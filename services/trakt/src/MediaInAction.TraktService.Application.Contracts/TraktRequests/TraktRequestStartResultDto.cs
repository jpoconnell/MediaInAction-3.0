using System;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestStartResultDto
{
    public string CheckoutLink { get; set; }
}