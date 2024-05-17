using System;

namespace MediaInAction.PaymentService.PaymentRequests;

[Serializable]
public class PaymentRequestStartResultDto
{
    public string CheckoutLink { get; set; }
}