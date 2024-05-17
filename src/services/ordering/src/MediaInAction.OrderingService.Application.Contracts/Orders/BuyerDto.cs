using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.OrderingService.Orders;

public class BuyerDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
}