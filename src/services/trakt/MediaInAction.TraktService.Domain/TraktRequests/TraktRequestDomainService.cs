using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktRequests;

public class TraktRequestDomainService : DomainService
{
    private readonly ITraktRequestRepository _traktRequestRepository;

    public TraktRequestDomainService(ITraktRequestRepository traktRequestRepository)
    {
        _traktRequestRepository = traktRequestRepository;
    }

    public async Task<TraktRequest> UpdateTraktRequestStateAsync(
        Guid traktRequestId,
        string requestStatus,
        string orderId)
    {
        var traktRequest = await _traktRequestRepository.GetAsync(traktRequestId);

        if (requestStatus == requestStatus)
        {
            traktRequest.SetAsCompleted();
        }
        else
        {
            traktRequest.SetAsFailed(requestStatus);
        }

    
        traktRequest.ExtraProperties[nameof(requestStatus)] = requestStatus;

        await _traktRequestRepository.UpdateAsync(traktRequest);

        return traktRequest;
    }
}