using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktEpisodeAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeManager : DomainService
    {
        private ILogger<TraktEpisodeManager> _logger;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly ITraktEpisodeRepository _traktEpisodeRepository;

        public TraktEpisodeManager(
            ITraktEpisodeRepository traktEpisodeRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TraktEpisodeManager> logger)
        {
            _traktEpisodeRepository = traktEpisodeRepository;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }
        
        
        public async Task<TraktEpisodeDto> CreateAsync(TraktEpisodeCreateDto traktEpisodeCreateDto)
        {
            try
            {
                if (traktEpisodeCreateDto.EpisodeName.IsNullOrEmpty())
                {
                    traktEpisodeCreateDto.EpisodeName = "Unknown";
                }
                
                /*
                if (season == null)
                {
                    return null;
                }
                */
                // Create new traktEpisode
                if (traktEpisodeCreateDto.TraktEpisodeCreateAliases == null)
                {
                    traktEpisodeCreateDto.TraktEpisodeCreateAliases = new List<( string idType, string idValue)>();
                }
                
                var episode = new TraktEpisode
                {
                    ShowSlug = traktEpisodeCreateDto.ShowSlug,
                    SeasonNum = traktEpisodeCreateDto.SeasonNum,
                    EpisodeNum = traktEpisodeCreateDto.EpisodeNum,
                    EpisodeName = traktEpisodeCreateDto.EpisodeName,
                    AiredDate = traktEpisodeCreateDto.AiredDate,
                    IsActive = true,
                    TraktEpisodeAliases = traktEpisodeCreateDto.TraktEpisodeCreateAliases
                };
                
                // Add new traktEpisode aliases
                foreach (var alias in traktEpisodeCreateDto.TraktEpisodeCreateAliases)
                {
                    if ((alias.idValue != null) && (alias.idType != null))
                    {
                        episode.TraktEpisodeAliases.Add((alias.idType,alias.idValue));
                    }
                }
                
                var traktEpisode = await _traktEpisodeRepository.InsertAsync(episode);
                if (traktEpisode != null)
                {
                    _logger.LogInformation("Trakt Episode Created:" + episode.ShowSlug + ":" +
                                 episode.SeasonNum.ToString() + ":" + episode.EpisodeNum.ToString() + episode.EpisodeName);
                }
                
                // Publish Episode created event
                var createdTraktEpisodeDto = MapToDto(traktEpisode);
                await SendCreateEvent(createdTraktEpisodeDto);
                return createdTraktEpisodeDto;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }

        private TraktEpisodeDto MapToDto(TraktEpisode traktEpisode)
        {
            var traktEpisodeDto = new TraktEpisodeDto
            {
                Id = traktEpisode.Id,
                ShowSlug = traktEpisode.ShowSlug,
                SeasonNum = traktEpisode.SeasonNum,
                EpisodeNum = traktEpisode.EpisodeNum,
                EpisodeName = traktEpisode.EpisodeName,
                AiredDate = traktEpisode.AiredDate,
                TraktEpisodeAliasDtos = traktEpisode.TraktEpisodeAliases
            };
            return traktEpisodeDto;
        }

        private async Task SendCreateEvent(TraktEpisodeDto episode)
        {
            if ((episode.EpisodeNum > 0) && (episode.SeasonNum > 0) && (episode.ShowSlug.Length > 0))
            {
                _logger.LogInformation("Sending TraktEpisodeCreated Event: " + 
                                       episode.ShowSlug + ":"+ episode.SeasonNum.ToString() +":" + episode.EpisodeNum.ToString());
                await _distributedEventBus.PublishAsync(new TraktEpisodeCreatedEto
                {
                    TraktId = episode.Id.ToString(),
                    ShowSlug = episode.ShowSlug,
                    SeasonNum = episode.SeasonNum,
                    EpisodeNum = episode.EpisodeNum,
                    EpisodeName = episode.EpisodeName,
                    AiredDate = episode.AiredDate,
                    TraktEpisodeCreatedAliases = episode.TraktEpisodeAliasDtos
                });
            }
        }

        private List<TraktEpisodeAliasCreatedEto> GetEpisodeAliasEtoList(List<TraktEpisodeAlias> traktEpisodeAliases)
        {
            List<TraktEpisodeAliasCreatedEto> etoList = new List<TraktEpisodeAliasCreatedEto>();
            foreach (var oItem in traktEpisodeAliases)
            {
                etoList.Add(new TraktEpisodeAliasCreatedEto()
                {
                    IdType = oItem.IdType,
                    IdValue = oItem.IdValue,
                });
            }
            return etoList;
        }

        public async Task<TraktEpisode> UpdateTraktStatus(Guid traktId, FileStatus status)
        {
            try
            {
                var traktEpisode = await _traktEpisodeRepository.GetAsync(traktId);
                if (traktEpisode.TraktStatus != status)
                {
                    traktEpisode.TraktStatus = status;
                    await _traktEpisodeRepository.UpdateAsync(traktEpisode, true);
                    return traktEpisode;
                }
            }
            catch 
            {
                _logger.LogDebug("TraktEpisodeManager.UpdateTraktStatus: TraktEpisode Not Found " + traktId.ToString());
                return null;
            }

            return null;
        }

        public async Task ResendEvent(TraktEpisodeDto episode)
        {
            await SendCreateEvent(episode);
        }
    }
}
