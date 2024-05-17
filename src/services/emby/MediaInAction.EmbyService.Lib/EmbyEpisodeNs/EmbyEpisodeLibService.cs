using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class EmbyEpisodeLibService : IEmbyEpisodeLibService
{
    private readonly ILogger<EmbyEpisodeLibService> _logger;
    private readonly EmbyEpisodeManager _episodeManager;
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public EmbyEpisodeLibService(
        ILogger<EmbyEpisodeLibService> logger,
        IEmbyEpisodeRepository embyEpisodeRepository,
        EmbyEpisodeManager embyEpisodeManager,
        IDistributedEventBus distributedEventBus
    )
    {
        _logger = logger;
        _embyEpisodeRepository = embyEpisodeRepository;
        _episodeManager = embyEpisodeManager;
        _distributedEventBus = distributedEventBus;
    }

    public async Task UpdateAddFromDto(EmbyEpisodeDto episode)
    {
        try 
        { 
            await CreateUpdateFolder(episode);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }

    public async Task<List<EmbyEpisodeDto>> GetAll()
    {
        var embySeriesList = await _embyEpisodeRepository.GetListAsync();
        var returnList = new List<EmbyEpisodeDto>();
        foreach (var embySeries in embySeriesList)
        {
            var episode = new EmbyEpisodeDto();
            //episode.Name = embySeries.Name;
            episode.EmbyId = embySeries.EmbyId;
            returnList.Add(episode);
        }

        return returnList;
    }

    private async Task CreateUpdateFolder(EmbyEpisodeDto episode)
    {
        try
        {
            var dbEpisode = await _embyEpisodeRepository.GetBySeriesSeasonEpisodeAsync(episode.EmbySeriesId,episode.SeasonNum,episode.EpisodeNum);
            if (dbEpisode == null)
            {
                var createdEpisode = await _episodeManager.CreateAsync(episode.ShowId,
                    episode.SeasonNum, episode.EpisodeNum);
            }
           
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
    
    public async Task SendAllEpisodesEventList()
    {
        var trakEpisodeListEtos = new List<EmbyEpisodeCreatedEto>();
            
        var embyEpisodes = await _embyEpisodeRepository.GetListAsync(true);
        foreach (var episode in embyEpisodes)
        {
            var embyEpisode = new EmbyEpisodeCreatedEto();
            embyEpisode.EmbyId = episode.Id.ToString();
         //   embyEpisode.EmbySeriesId = episode.SeriesId.ToString();
            embyEpisode.SeasonNum = episode.SeasonNum;
            embyEpisode.EpisodeNum = episode.EpisodeNum;
            trakEpisodeListEtos.Add(embyEpisode);
        }

        foreach (var embyEpisodeCreateEto in trakEpisodeListEtos)
        {
            await _distributedEventBus.PublishAsync(embyEpisodeCreateEto);
        }
    }
}

