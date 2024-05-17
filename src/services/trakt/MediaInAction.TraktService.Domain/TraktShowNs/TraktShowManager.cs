using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowManager : DomainService
    {
        private readonly ITraktShowRepository _showRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private ILogger<TraktShowManager> _logger;
        
        public TraktShowManager(
            ITraktShowRepository traktShowRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TraktShowManager> logger
        )
        {
            _showRepository = traktShowRepository;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }

        public async Task<TraktShow> CreateAsync(TraktShowCreateDto traktShowCreateDto)
        {
            var existingShow = await _showRepository.FirstOrDefaultAsync(p => p.Name == traktShowCreateDto.Name);
            if (existingShow != null)
            {
                var updatedShow = new TraktShowDto();
                updatedShow.Id = existingShow.Id;
                updatedShow.Name = traktShowCreateDto.Name;
                updatedShow.FirstAiredYear = traktShowCreateDto.FirstAiredYear;
                updatedShow.Slug = traktShowCreateDto.Slug;
                foreach (var createAlias in traktShowCreateDto.TraktShowCreatedAliases)
                {
                    var found = false;
                    foreach( var existingAlias in existingShow.TraktShowAliases)
                    {
                        if (createAlias.idType == existingAlias.idType)
                        {
                            if (createAlias.idValue != existingAlias.idValue)
                            {
                                found = true;
                            }
                        }
                    }

                    if (found == false)
                    {
                        existingShow.TraktShowAliases.Add(createAlias);
                        var updatedShowDto = new TraktShowDto();
                        updatedShowDto.Id = existingShow.Id;
                        updatedShowDto.Name = existingShow.Name;
                        updatedShowDto.FirstAiredYear = existingShow.FirstAiredYear;
                        updatedShowDto.Slug = existingShow.Slug;
                        foreach (var existShowAlias in  existingShow.TraktShowAliases)
                        {
                            var newAlias = new TraktShowAliasDto();
                            newAlias.IdType = existShowAlias.idType;
                            newAlias.IdValue = existShowAlias.idValue;
                            updatedShowDto.TraktShowAliasDtos.Add(newAlias);
                        }
                        await UpdateTraktShowAsync(updatedShowDto);
                    }
                }
            }
            
            var newTraktShow = new TraktShow(
                GuidGenerator.Create(),
                traktShowCreateDto.Name,
                traktShowCreateDto.FirstAiredYear,
                traktShowCreateDto.TraktShowCreatedAliases,
                traktShowCreateDto.Slug
            );
            await SendCreateEvent(newTraktShow);
            return await _showRepository.InsertAsync(newTraktShow);
        }

        public async Task SendCreateEvent(TraktShow show)
        {
            // Publish Trakt Show create event
            _logger.LogInformation("Sending TraktShowCreated Event: "  );
            await _distributedEventBus.PublishAsync(new TraktShowCreatedEto
            {
                TraktId = show.Id.ToString() ,
                Slug = show.Slug ,
                Name = show.Name,
                FirstAiredYear = show.FirstAiredYear,
                TraktShowCreatedAliases = show.TraktShowAliases
            });
        }

        public async Task<TraktShow> UpdateTraktShowAsync(TraktShowDto traktShowDto)
        {
            var existingShow = await _showRepository.FirstOrDefaultAsync(p => p.Slug == traktShowDto.Slug);
            // TODO: check for changes here
            
            // TODO: send update event
            var createdTraktShow = await _showRepository.UpdateAsync(existingShow, true);
            return createdTraktShow;
        }
        
        public async Task UpdateTraktMovieStatusAsync(TraktShowDto dbShow, FileStatus status)
        {
            dbShow.TraktStatus  = status;
            await UpdateTraktShowAsync(dbShow);
        }

        public async Task ResendEvent(TraktShow show)
        {
            await SendCreateEvent(show);
        }
    }
}
