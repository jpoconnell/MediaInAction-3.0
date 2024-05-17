using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestCreationDto
{
    [Required]
    [MaxLength(TraktRequestConsts.MaxCurrencyLength)]
    public string Currency { get; set; }

    [Required]
    [MinLength(TraktRequestConsts.MinOrderIdLength)]
    [MaxLength(TraktRequestConsts.MaxOrderIdLength)]
    public string OrderId { get; set; }

    [Required]
    public int OrderNo { get; set; }

    public string BuyerId { get; set; }
    public List<TraktRequestProductCreationDto> Products { get; set; }
}