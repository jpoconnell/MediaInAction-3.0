using System;

namespace MediaInAction.PaymentService.PaymentRequests;

[Serializable]
public class PaymentRequestCompleteInputDto
{
    public string Token { get; set; }
    public int PaymentTypeId { get; set; }
}