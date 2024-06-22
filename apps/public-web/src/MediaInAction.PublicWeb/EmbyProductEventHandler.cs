using System.Threading.Tasks;
using MediaInAction.EmbyService.Services;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.PublicWeb;

public class EmbyProductEventHandler : IDistributedEventHandler<EmbyProductUpdatedEto>, ITransientDependency
{
    private readonly IHubContext<EmbyHub> _hubContext;

    public EmbyProductEventHandler(IHubContext<EmbyHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task HandleEventAsync(EmbyProductUpdatedEto eventData)
    {
        await _hubContext.Clients.All.SendAsync("EmbyProductUpdated", eventData);
    }
}