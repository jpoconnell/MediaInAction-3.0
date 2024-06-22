using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestProductCreationDto
{
    public string ReferenceId { get; set; }

    [Required]
    [MaxLength(TraktRequestConsts.MaxCodeLength)]
    public string Code { get; set; }

    [Required]
    [MaxLength(TraktRequestConsts.MaxNameLength)]
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}