using MediaInAction.FileService.OrderItems;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.FileService.Orders;

public class DashboardDto: EntityDto
{
    public List<TopSellingDto> TopSellings { get; set; }
    public List<TraktDto> Trakts { get; set; }
    public List<OrderStatusDto> OrderStatusDto { get; set; }
}