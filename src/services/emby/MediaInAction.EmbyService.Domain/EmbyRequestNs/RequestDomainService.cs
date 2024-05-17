using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyRequestNs;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.RequestNs;

public class RequestDomainService : DomainService
{
    private readonly IRequestRepository _requestRepository;

    public RequestDomainService(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<Request> UpdateRequestStateAsync(
        Guid requestId,
        EmbyRequestState requestStatus)
    {
        var request = await _requestRepository.GetAsync(requestId);

        if (requestStatus == EmbyRequestState.Completed )
        {
            request.SetAsCompleted();
        }
        else
        {
            request.SetAsFailed("Failed");
        }

        await _requestRepository.UpdateAsync(request);

        return request;
    }
}