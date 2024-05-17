using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class EmbyEpisodeManager : DomainService
{
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;
    private ILogger<EmbyEpisodeManager> _logger;
    
    public EmbyEpisodeManager(
        IEmbyEpisodeRepository embyEpisodeRepository,
        ILogger<EmbyEpisodeManager> logger
    )
    {
        _embyEpisodeRepository = embyEpisodeRepository;
        _logger = logger;
    }

    public async Task<EmbyEpisode> CreateAsync(
        string showId,
        int seasonNum,
        int episodeNum
    )
    {
        var episode = new EmbyEpisode()
        {
            ShowId = showId,
            SeasonNum = seasonNum,
            EpisodeNum = episodeNum
        };

        try
        {
            var createdEpisode = await _embyEpisodeRepository.InsertAsync(episode, true);
            return createdEpisode;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
