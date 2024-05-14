using System.Collections.Generic;

namespace MediaInAction.OrderingService.Orders;

public class OrderCreateDto
{
    public string PaymentMethod { get; set; }
    public OrderAddressDto Address { get; set; } = new();
    public List<OrderItemCreateDto> Products { get; set; } = new();
}