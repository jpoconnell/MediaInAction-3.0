using System;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.PaymentService.PaymentRequests
{
    public interface IPaymentRequestRepository : IBasicRepository<PaymentRequest, Guid>
    {
    }
}
