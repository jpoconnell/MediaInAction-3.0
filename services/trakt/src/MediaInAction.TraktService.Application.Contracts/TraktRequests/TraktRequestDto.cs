using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktRequests;

[Serializable]
public class TraktRequestDto : CreationAuditedEntityDto<Guid>
{
    [Required]
    [MaxLength(TraktRequestConsts.MaxCurrencyLength)] 
    public string Currency { get; set; }

    public string OrderId { get; set; }
        
    public int OrderNo {get;set;}
        
    public string BuyerId { get; set; }

    public bool IsDeleted { get; set; }

    public TraktRequestState State { get; set; }

    public List<TraktRequestProductDto> Products { get; set; }
}