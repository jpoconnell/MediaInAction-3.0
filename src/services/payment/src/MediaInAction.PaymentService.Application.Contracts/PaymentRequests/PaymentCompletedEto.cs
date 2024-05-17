using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.PaymentService.PaymentRequests;

[EventName("MediaInAction.Payment.Completed")]
public class PaymentCompletedEto : EtoBase
{
    public PaymentRequestDto PaymentRequest { get; set; }
}