using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktRequests;

public class TraktRequestProductDto : EntityDto<Guid>
{
    public Guid TraktRequestId { get; set; }

    public string ReferenceId { get; set; }
        
    public string Code { get; set; }
        
    public string Name { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

}