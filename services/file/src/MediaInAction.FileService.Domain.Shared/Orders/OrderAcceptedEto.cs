using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.FileService.Orders;

[Serializable]
[EventName("MediaInAction.Order.Accepted")]
public class OrderAcceptedEto : EtoBase
{
    public Guid OrderId { get; set; }
    public string TraktStatus { get; set; }
    public BuyerEto Buyer { get; set; }
    public List<OrderItemEto> Items { get; set; }
}