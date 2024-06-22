using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktRequests;

public class TraktRequestDomainService : DomainService
{
    private readonly ITraktRequestRepository _paymentRequestRepository;

    public TraktRequestDomainService(ITraktRequestRepository paymentRequestRepository)
    {
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<TraktRequest> UpdateTraktRequestStateAsync(
        Guid paymentRequestId,
        string orderStatus,
        string orderId)
    {
        var paymentRequest = await _paymentRequestRepository.GetAsync(paymentRequestId);

        if (orderStatus == PayPalConsts.OrderStatus.Completed || orderStatus == PayPalConsts.OrderStatus.Approved)
        {
            paymentRequest.SetAsCompleted();
        }
        else
        {
            paymentRequest.SetAsFailed(orderStatus);
        }

        paymentRequest.ExtraProperties[PayPalConsts.OrderIdPropertyName] = orderId;
        paymentRequest.ExtraProperties[nameof(orderStatus)] = orderStatus;

        await _paymentRequestRepository.UpdateAsync(paymentRequest);

        return paymentRequest;
    }
}