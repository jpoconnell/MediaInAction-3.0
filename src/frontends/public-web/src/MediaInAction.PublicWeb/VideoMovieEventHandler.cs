using System.Threading.Tasks;
using MediaInAction.BasketService.Services;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.PublicWeb;

public class VideoMovieEventHandler : IDistributedEventHandler<BasketProductUpdatedEto>, ITransientDependency
{
    private readonly IHubContext<MovieHub> _hubContext;

    public VideoMovieEventHandler(IHubContext<MovieHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task HandleEventAsync(VideoMovieUpdatedEto eventData)
    {
        await _hubContext.Clients.All.SendAsync("VideoMovieUpdated", eventData);
    }
}