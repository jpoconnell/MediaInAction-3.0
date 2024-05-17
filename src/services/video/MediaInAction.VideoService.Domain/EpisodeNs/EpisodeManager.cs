using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.EpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs;
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
        
    public async Task<Episode> CreateAsync(
        Guid seriesId,
        int seasonNum,
        int episodeNum,
        List<( string idType, string idValue)> episodeAliases,
        DateTime airedDate,
        string source = "",
        string episodeName = "",
        string altEpisodeId = "",
        string seasonEpisode = ""
    )
    {
        var addAiredDate = Convert.ToDateTime("1/1/2000");

        if (airedDate != null)
        {
            addAiredDate = airedDate;
        }
        // Create new episode
        Episode episode = new Episode(
            id: GuidGenerator.Create(),
            seriesId: seriesId,
            seasonNum: seasonNum,
            episodeNum: episodeNum,
            airedDate: addAiredDate,
            episodeName: episodeName,
            seasonEpisode: seasonEpisode);
        
        // Add new episode aliases
        if (episodeAliases == null )
        {
            episode.AddEpisodeAlias(
                id: GuidGenerator.Create(),
                episodeId: episode.Id,
                idType: "name",
                idValue: "name"
            );
        }
        else if (episodeAliases.Count == 0)
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
            foreach (var episodeAlias in episodeAliases)
            {
                episode.AddEpisodeAlias(
                    id: GuidGenerator.Create(),
                    episodeId: episode.Id,
                    idType: episodeAlias.idType,
                    idValue: episodeAlias.idValue
                );
            }
        }

        try
        {
            var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(seriesId,
                seasonNum, episodeNum,true);

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

    public async Task<Episode> AcceptTraktEpisodeAsync(TraktService.TraktEpisodeNs.TraktEpisodeCreatedEto input, Guid seriesId )
    {
        _logger.LogInformation("AcceptTraktEpisodeAsync");
      
        var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, input.SeasonNum, input.EpisodeNum);
        
        if (dbEpisode == null)
        {
            //var episodeAliases = MapAliases(input.TraktEpisodeCreatedAliases);
            
            var episodeCreate = new EpisodeCreateDto();
            episodeCreate.EpisodeCreateAliases = input.TraktEpisodeCreatedAliases;
            episodeCreate.SeasonNum = input.SeasonNum;
            episodeCreate.EpisodeNum = input.EpisodeNum;
            episodeCreate.AiredDate = input.AiredDate;
            episodeCreate.EpisodeName = input.EpisodeName;
            var newEpisode = await CreateAsync(episodeCreate);

            return newEpisode;
        }
        else  // is an update
        {
            var slugName =   ":S" + input.SeasonNum.ToString() + "E" + input.EpisodeNum.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                
                _logger.LogInformation("Update Try");
                
                var updatedEpisode =  await _episodeRepository.UpdateAsync(dbEpisode, autoSave: true);
                return updatedEpisode;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }


    public async Task<Episode> AcceptEmbyEpisodeAsync(EmbyEpisodeCreatedEto input, Guid seriesId)
    {
        _logger.LogInformation("AcceptEmbyEpisodeAsync");
        
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, input.SeasonNum, input.EpisodeNum);

        var aliases = MapAliases(input.Aliases);
        if (episode == null)
        {
            var episodeCreate = new EpisodeCreateDto();
            episodeCreate.SeasonNum = input.SeasonNum;
            episodeCreate.EpisodeNum = input.EpisodeNum;
            episodeCreate.AiredDate = input.AiredDate;
            episodeCreate.EpisodeName = input.EpisodeName;
            
            var newEpisode = await CreateAsync(episodeCreate);
            return newEpisode;
        }
        else  // is an update
        {
            var slugName =   ":S" +  input.SeasonNum.ToString() + "E" + input.EpisodeNum.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                _logger.LogInformation("Update Try");
                
                var id =  await _episodeRepository.UpdateAsync(episode, autoSave: true);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }
    
    private List<EpisodeAliasCreateDto> MapAliases(List<TraktEpisodeAliasCreatedEto> aliases)
    {
        var episodeAliasList = new List<EpisodeAliasCreateDto>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAliasCreateDto();
                newEpisodeAlias.IdType = alias.IdType;
                newEpisodeAlias.IdValue = alias.IdValue;
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        return episodeAliasList;
    }
    
    private List<EpisodeAliasCreateDto> MapAliases(List<EmbyEpisodeAliasCreatedEto> aliases)
    {
        var episodeAliasList = new List<EpisodeAliasCreateDto>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAliasCreateDto();
                newEpisodeAlias.IdType = alias.IdType;
                newEpisodeAlias.IdValue = alias.IdValue;
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        
        return episodeAliasList;
    }
    
    private List<EpisodeAliasCreatedEto> MapAliases(List<EpisodeAlias> aliases)
    {
        var episodeAliasList = new List<EpisodeAliasCreatedEto>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAliasCreatedEto();
                newEpisodeAlias.IdType = alias.IdType;
                newEpisodeAlias.IdValue = alias.IdValue;
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        return episodeAliasList;
    }

    public async Task<Guid> AcceptTraktEpisodeAsync(
        TraktEpisodeAcknowledgeEto eventData, 
        Guid seriesId)
    {
        _logger.LogInformation("Acknowledge EmbyEpisodeAsync");
        
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, eventData.Season, eventData.Episode);
        
        if (episode != null)
        {
            var slugName =   ":S" +  eventData.Season.ToString() + "E" + eventData.Episode.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                episode.EventStatus = FileStatus.Accepted;
                var updatedEpisode =  await _episodeRepository.UpdateAsync(episode, autoSave: true);
                return updatedEpisode.Id;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return Guid.Empty;
            }
        }
        else
        {
            _logger.LogDebug("Should be an episode for this");
            return Guid.Empty;
        }
    }
}
