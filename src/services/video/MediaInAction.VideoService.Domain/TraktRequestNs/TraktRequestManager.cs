using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.TraktRequestNs;

public class TraktRequestManager : DomainService
{
    private readonly ITraktRequestRepository _traktRequestRepository;
    private readonly ILogger<TraktRequestManager> _logger;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public TraktRequestManager(ITraktRequestRepository traktRequestRepository,
        ILogger<TraktRequestManager> logger,
        IDistributedEventBus distributedEventBus)
    {
        _traktRequestRepository = traktRequestRepository;
        _distributedEventBus = distributedEventBus;
        _logger = logger;
    }
 
    public async Task<List<TraktRequest>> GetUnCompleteRequests()
    {
        return await _traktRequestRepository.GetUnCompleteRequests();
    }

    public async Task<TraktRequest> CreateAsync(string command, List<TraktRequestItem> items)
    {
        var traktRequest = new TraktRequest(Guid.NewGuid(), command);
        traktRequest.RequestItems = items;
        var returnValue = await _traktRequestRepository.InsertAsync(traktRequest,true);
        return returnValue;
    }
}
