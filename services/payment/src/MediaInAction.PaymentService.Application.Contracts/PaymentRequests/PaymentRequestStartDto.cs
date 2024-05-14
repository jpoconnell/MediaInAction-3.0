using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.PaymentService.PaymentRequests;

[Serializable]
public class PaymentRequestStartDto
{
    public int PaymentTypeId { get; set; }
    public Guid PaymentRequestId { get; set; }

    [Required]
    public string ReturnUrl { get; set; }

    public string CancelUrl { get; set; }
}