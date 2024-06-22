using System.Collections.Generic;

namespace MediaInAction.FileService.Orders;

public class OrderCreateDto
{
    public string TraktMethod { get; set; }
    public OrderAddressDto Address { get; set; } = new();
    public List<OrderItemCreateDto> Products { get; set; } = new();
}