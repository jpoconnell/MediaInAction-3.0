using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.ShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeLibService : ITraktEpisodeLibService
{
    private readonly ILogger<TraktEpisodeLibService> _logger;
    private readonly TraktEpisodeManager _traktEpisodeManager;
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly IDistributedEventBus _eventBus;
    
    public TraktEpisodeLibService(
        ILogger<TraktEpisodeLibService> logger,
        TraktEpisodeManager traktEpisodeManager,
        ITraktEpisodeRepository traktEpisodeRepository,
        IDistributedEventBus eventBus
    )
    {
        _traktEpisodeManager = traktEpisodeManager;
        _traktEpisodeRepository = traktEpisodeRepository;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task UpdateAddFromDto(CollectionEpisodeDto episodeDto)
    {
        try
        {
            var dbShowId = await CreateUpdateEpisode(episodeDto);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:UpdateAddFromDto"  +ex.Message);
        }
    }

    
    public async Task<Guid> CreateUpdateEpisode(CollectionEpisodeDto episodeDto)
    {
        var returnEpisode = new TraktEpisodeDto();
        var episodeAliases1 = new List<( string idType, string idValue)>();
        //var episodeAliases2 = new List<TraktEpisodeAlias>();
        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            episodeAliases1.Add((alias.IdType, alias.IdValue));
            var newAlias = new TraktEpisodeAlias();
            newAlias.IdType = alias.IdType;
            newAlias.IdValue = alias.IdValue;
           // episodeAliases2.Add(newAlias);
        }

        var dbEpisode = await _traktEpisodeRepository.GetByTraktShowSlugSeasonEpisodeAsync(
            episodeDto.ShowSlug,
            episodeDto.SeasonNum, 
            episodeDto.EpisodeNum);
        if (dbEpisode == null)
        {
            if (Convert.ToDateTime(episodeDto.AiredDate) < DateTime.Now.AddYears(-5))
            {

            }

            var createEpisode = new TraktEpisodeCreateDto();
            createEpisode.ShowSlug = episodeDto.ShowSlug;
            createEpisode.SeasonNum = episodeDto.SeasonNum;
            createEpisode.EpisodeNum = episodeDto.EpisodeNum;
            createEpisode.EpisodeName = episodeDto.EpisodeName;
            createEpisode.AiredDate = episodeDto.AiredDate;
            createEpisode.TraktEpisodeCreateAliases = episodeAliases1;
            
            var createdEpisode = await _traktEpisodeManager.CreateAsync(createEpisode);
            if (createdEpisode != null)
            {
                _logger.LogInformation("Episode created:" + episodeDto.ShowSlug +
                                       ":" + episodeDto.SeasonNum.ToString() + ":" + episodeDto.EpisodeNum);
            }
            return createdEpisode.Id;
        }
        else
        {
            var returnId = Guid.Empty;
            var diff =  CompareTraktEpisode(dbEpisode, episodeDto);

            if (diff == true)
            {
                returnId = await UpdateTrakEpisode(dbEpisode, episodeDto);
            }
            else
            {
                returnId = Guid.Empty;
            }
            return returnId;
        }
    }

    private async Task<Guid> UpdateTrakEpisode(TraktEpisode dbEpisode, 
        CollectionEpisodeDto episodeDto)
    {
        var updatedShow = dbEpisode;
        updatedShow.ShowSlug = episodeDto.ShowSlug;
        updatedShow.SeasonNum = episodeDto.SeasonNum;
        updatedShow.EpisodeNum = episodeDto.EpisodeNum;
        updatedShow.AiredDate = episodeDto.AiredDate;
        updatedShow.EpisodeName = episodeDto.EpisodeName;
        
        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in dbEpisode.TraktEpisodeAliases)
            {
                if ((dbAlias.idType == alias.IdType) && (dbAlias.idValue == alias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                dbEpisode.TraktEpisodeAliases.Add((alias.IdType,alias.IdValue));
                _logger.LogInformation("Alias Added:" + alias.IdType +":"+ alias.IdValue); 
            }
        }

        var episode = await _traktEpisodeRepository.UpdateAsync(updatedShow,true);
        var updatedEpisode =  MapToDto(episode);
        await SendEpisodeUpdateEventAsync(updatedEpisode);
        return dbEpisode.Id;
    }

    private async Task SendEpisodeUpdateEventAsync(TraktEpisodeDto updatedShow)
    {
        _logger.LogInformation("Sending TraktEpisodeUpdated Event: " +
                               updatedShow.ShowSlug + ":"+ updatedShow.EpisodeNum );
        await _eventBus.PublishAsync(new TraktEpisodeUpdatedEto
        {
            ShowSlug = updatedShow.ShowSlug,
            SeasonNum = updatedShow.SeasonNum,
            EpisodeNum = updatedShow.EpisodeNum,
            EpisodeName = updatedShow.EpisodeName,
            AiredDate = updatedShow.AiredDate,
            TraktUpdatedEtoEpisodeAliases = updatedShow.TraktEpisodeAliasDtos
        });
    }

    private List<TraktEpisodeAliasUpdatedEto> MapAliases(List<TraktEpisodeAlias> episodeAliasList)
    {
        var returnList =  new List<TraktEpisodeAliasUpdatedEto>();
        foreach (var episodeAlias in episodeAliasList)
        {
            var newAlias = new TraktEpisodeAliasUpdatedEto();
            newAlias.IdType = episodeAlias.IdType;
            newAlias.IdValue = episodeAlias.IdValue;
            returnList.Add(newAlias);
        }

        return returnList;
    }

    private bool CompareTraktEpisode(TraktEpisode dbEpisode, 
        CollectionEpisodeDto episodeDto)
    {
        bool diff = dbEpisode.TraktEpisodeAliases.Count != episodeDto.CollectionEpisodeAliasDtos.Count;

        if (dbEpisode.ShowSlug != episodeDto.ShowSlug)
        {
            diff = true;
        }
            
        if (dbEpisode.SeasonNum != episodeDto.SeasonNum)
        {
            diff = true;
        }
        if (dbEpisode.EpisodeNum != episodeDto.EpisodeNum)
        {
            diff = true;
        }
            
        if (dbEpisode.AiredDate != episodeDto.AiredDate)
        {
            diff = true;
        }
        
        if (dbEpisode.EpisodeName != episodeDto.EpisodeName)
        {
            diff = true;
        }
        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in episodeDto.CollectionEpisodeAliasDtos)
            {
                if (dbAlias.IdType == alias.IdType)
                {
                    found = true;
                }
            }

            if (found == false)
            {
                diff = true;
            }
        }

        var tcs = new TaskCompletionSource<bool>(diff);
        return diff;
    }

    public async Task<List<TraktEpisodeDto>> GetListAsync()
    {
       var traktEpisodes = await _traktEpisodeRepository.GetListAsync();
       var traktEpisodeDtos = MapToDtos(traktEpisodes);
       return traktEpisodeDtos;
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodeByShow(string slug)
    {
        var traktEpisodes = await _traktEpisodeRepository.GetEpisodesByShow(slug);
        if (traktEpisodes != null)
        {
            var traktEpisodeDtos = MapToDtos(traktEpisodes);
            return traktEpisodeDtos;
        }
        else
        {
            return null;
        }
    }

    public async Task ResendUnAcceptedEpisodesList()
    {
        var episodes = await _traktEpisodeRepository.GetListAsync();
        foreach (var episode in episodes)
        {
            if (episode.TraktStatus == FileStatus.New)
            {
                var episodeDto = MapToDto(episode);
                await _traktEpisodeManager.ResendEvent(episodeDto);
            }
        }
    }

    private List<TraktEpisodeDto> MapToDtos(List<TraktEpisode> traktEpisodes)
    {
        var traktEpisodeDtos = new  List<TraktEpisodeDto>();
        foreach (var traktEpisode in traktEpisodes)
        {
            traktEpisodeDtos.Add(MapToDto(traktEpisode));     
        }

        return traktEpisodeDtos;
    }

    private TraktEpisodeDto MapToDto(TraktEpisode traktEpisode)
    {
        var traktEpisodeDto = new TraktEpisodeDto
        {
            ShowSlug = traktEpisode.ShowSlug,
            SeasonNum = traktEpisode.SeasonNum,
            EpisodeNum = traktEpisode.EpisodeNum,
            AiredDate = traktEpisode.AiredDate,
        };

        foreach (var episodeAlias in traktEpisode.TraktEpisodeAliases)
        {
            traktEpisodeDto.TraktEpisodeAliasDtos.Add((episodeAlias.idType,episodeAlias.idValue));
        }
        return traktEpisodeDto;
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodes()
    {
        var episodeList = await _traktEpisodeRepository.GetListAsync();
        var episodeListDto = new List<TraktEpisodeDto>();
        foreach (var episode in episodeList)
        {
            var episodeDto = new TraktEpisodeDto();
            episodeDto.EpisodeNum = episode.EpisodeNum;
            episodeDto.SeasonNum = episode.SeasonNum;
            episodeDto.ShowSlug = episode.ShowSlug;
            episodeDto.AiredDate = episode.AiredDate;
            episodeListDto.Add(episodeDto);
        }

        return episodeListDto;
    }
    
}
