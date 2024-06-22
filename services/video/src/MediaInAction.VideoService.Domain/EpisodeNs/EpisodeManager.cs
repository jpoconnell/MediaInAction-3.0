using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeManager : DomainService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<EpisodeManager> _logger; 
    private readonly ISeriesRepository _seriesRepository;
    
    public EpisodeManager(IEpisodeRepository episodeRepository,
        ISeriesRepository seriesRepository,
        ILogger<EpisodeManager> logger,
        IDistributedEventBus eventBus)
    {
        _episodeRepository = episodeRepository;
        _seriesRepository = seriesRepository;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task<Episode> CreateAsync(EpisodeCreateDto episodeCreateDto)
    {
        var addAiredDate = Convert.ToDateTime("1/1/2000");

        if (episodeCreateDto.AiredDate == null)
        {
            episodeCreateDto.AiredDate = addAiredDate;
        }
        // Find series by name and year
        var seriesYear = Convert.ToInt32(episodeCreateDto.SeriesYear);
        var dbSeries = await _seriesRepository.FindBySeriesNameYear(episodeCreateDto.SeriesName, seriesYear);
        
        if (dbSeries == null)
        {
            _logger.LogInformation("Series not found");
            return null;
        }
        else
        {
            // Create new episode
            var episode = new Episode(
                id: GuidGenerator.Create(),
                seriesId: dbSeries.Id,
                seasonNum: episodeCreateDto.SeasonNum,
                episodeNum: episodeCreateDto.EpisodeNum,
                airedDate: (DateTime)episodeCreateDto.AiredDate,
                episodeName: "",
                seasonEpisode: ""
                );
        
            // Add new episode aliases
            if (episodeCreateDto.EpisodeCreateAliases == null )
            {
                episode.AddEpisodeAlias(
                    id: GuidGenerator.Create(),
                    episodeId: episode.Id,
                    idType: "name",
                    idValue: "name"
                );
            }
            else if (episodeCreateDto.EpisodeCreateAliases.Count == 0)
            {
                episode.AddEpisodeAlias(
                    id: GuidGenerator.Create(),
                    episodeId: episode.Id,
                    idType: "creationDate",
                    idValue: DateTime.Now.ToString()
                );  
            }
            else
            {
                foreach (var episodeAlias in episodeCreateDto.EpisodeCreateAliases)
                {
                    //TODO check for duplicates
                    
                    //episode.AddEpisodeAlias(episodeAlias.id,episodeAlias.idType, episodeAlias.idValue);
                }
            }
            try
            {
                var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(dbSeries.Id,
                    episode.SeasonNum, episode.EpisodeNum,true);

                if (dbEpisode != null)
                {
                    UpdateEpisode(dbEpisode,episode);
                    return dbEpisode;
                }
                else {
                    var createdEpisode = await _episodeRepository.InsertAsync(episode, true);
                    var createdEpisodeAliases = MapAliases(createdEpisode.EpisodeAliases);
                    // Create Episode event
                    await _eventBus.PublishAsync(new EpisodeCreatedEto
                    {
                        EpisodeId = createdEpisode.Id.ToString(),
                        SeriesId = createdEpisode.SeriesId.ToString(),
                        SeasonNum = createdEpisode.SeasonNum,
                        EpisodeNum = createdEpisode.EpisodeNum,
                        EpisodeAliases = createdEpisodeAliases
                    });

                    return createdEpisode;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }

    private List<EpisodeAliasCreatedEto> MapAliases(List<EpisodeAlias> createdEpisodeEpisodeAliases)
    {
        throw new NotImplementedException();
    }


    private Episode UpdateEpisode(Episode dbEpisode, Episode episode)
    {
        var updated = 0; 
        if (dbEpisode.AiredDate != episode.AiredDate)
        {
            if (episode.AiredDate > Convert.ToDateTime("01/01/2020"))
            {
                dbEpisode.AiredDate = episode.AiredDate;
                updated++;
            }
        }

        foreach (var episodeAlias in episode.EpisodeAliases)
        {
            var found = false;
            foreach (var dbEpisodeAlias in dbEpisode.EpisodeAliases)
            {
                if ((dbEpisodeAlias.IdType == episodeAlias.IdType) &&
                    (dbEpisodeAlias.IdValue == episodeAlias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                if (episodeAlias.IdType != "tmdb")
                {
                    dbEpisode.EpisodeAliases.Add(episodeAlias);
                    updated++;
                }
            }
        }

        if (updated > 0)
        {
            _episodeRepository.UpdateAsync(dbEpisode);
        }

        return dbEpisode;
    }
    
    public async Task SetStatusAsync(Guid episodeId, MediaStatus status)
    {
       var episode = await _episodeRepository.GetAsync(episodeId);
       episode.SetEpisodeStatus(status);
       await _episodeRepository.UpdateAsync(episode, true);
    }
    
    public async Task<Episode> UpdateAsync(EpisodeCreateDto episodeCreateDto)
    {
        throw new NotImplementedException();
    }

    public async Task AddEpisodeAliasAsync(object id, EpisodeAliasCreateDto episodeAliasCreateDto)
    {
        throw new NotImplementedException();
    }
}
