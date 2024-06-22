using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestStartDto
{
    public int TraktTypeId { get; set; }
    public Guid TraktRequestId { get; set; }

    [Required]
    public string ReturnUrl { get; set; }

    public string CancelUrl { get; set; }
}