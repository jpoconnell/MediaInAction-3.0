using Volo.Abp.Application.Dtos;

namespace MediaInAction.OrderingService.Orders;

public class PaymentDto : EntityDto
{
    public decimal RateOfPaymentMethod { get; set; }
    public string PaymentMethod { get; set; }
}