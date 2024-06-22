using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.Orders;

[EventName("MediaInAction.Order.Cancelled")]
public class OrderCancelledEto : EtoBase
{
    public Guid TraktRequestId { get; set; }
    public Guid OrderId { get; set; }
    public int OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public BuyerEto Buyer { get; set; }
    public List<OrderItemEto> Items { get; set; } = new();
}