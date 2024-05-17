using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.EmbyService.EventManagement;

public class EmbySendAllService : IEmbySendAllService  
{
    private readonly IEmbyShowLibService _embyShowLibService;
    private readonly IEmbyEpisodeLibService _embyEpisodeLibService;
    private readonly IEmbyMovieLibService _embyMovieLibService;
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly ILogger<EmbySendAllService> _logger;

    public EmbySendAllService(
        ILogger<EmbySendAllService> logger,
        IEmbyShowLibService embyShowLibService,
        IEmbyMovieLibService embyMovieLibService,
        IEmbyEpisodeLibService embyEpisodeLibService,
        IDistributedEventBus distributedEventBus)
    {
        _logger = logger;
        _embyShowLibService = embyShowLibService;
        _embyMovieLibService = embyMovieLibService;
        _embyEpisodeLibService = embyEpisodeLibService;
        _distributedEventBus = distributedEventBus;
    }
    
    public async Task SendAllMovies()
    {
        await _embyMovieLibService.SendAllMoviesEventList();
    }
    
    public async Task SendAllShows()
    {
        await _embyShowLibService.SendAllShowsEventList();
        await _embyEpisodeLibService.SendAllEpisodesEventList();
    }
}