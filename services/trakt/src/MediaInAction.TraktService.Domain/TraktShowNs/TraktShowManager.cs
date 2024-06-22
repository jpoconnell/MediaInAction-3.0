using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktShowAliasNs;
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
            if (existingShow == null)
            {
                var newTraktShow = new TraktShow(
                    GuidGenerator.Create(),
                    traktShowCreateDto.Name,
                    traktShowCreateDto.FirstAiredYear,
                    traktShowCreateDto.TraktShowCreatedAliases,
                    traktShowCreateDto.Slug
                );
                var returnValue = await _showRepository.InsertAsync(newTraktShow);
               // await SendCreateEvent(newTraktShow);
                //await SendCreateGrpc(newTraktShow);
                return returnValue;
            }
            else
            {
                return null;
            }
        }

        private async Task SendCreateGrpc(TraktShow newTraktShow)
        {
            throw new NotImplementedException();
        }

        private async Task<TraktShowDto> TranslateToTraktShowDto(TraktShow existingShow, TraktShowCreateDto traktShowCreateDto)
        {
            var traktShowDto = new TraktShowDto
            {
                TraktStatus = existingShow.TraktStatus,
                Id = existingShow.Id
            };
            var slug = "";
            if (existingShow.Slug.IsNullOrEmpty())
            {
                slug = existingShow.Slug;
                if (slug.IsNullOrEmpty())
                {
                    foreach (var existingShowTraktShowAlias in existingShow.TraktShowAliases)
                    {
                        if (existingShowTraktShowAlias.idType == "slug")
                        {
                            slug = existingShowTraktShowAlias.idValue;
                        }
                    }
                }

                if (slug.IsNullOrEmpty())
                {
                    if (!traktShowCreateDto.Slug.IsNullOrEmpty())
                    {
                        slug = traktShowCreateDto.Slug;
                    }
                }
                if (slug.IsNullOrEmpty())
                {
                    foreach (var traktShowCreatedAlias in traktShowCreateDto.TraktShowCreatedAliases)
                    {
                        if (traktShowCreatedAlias.idType == "slug")
                        {
                            slug = traktShowCreatedAlias.idValue;
                        }
                    }
                }

                traktShowDto.Slug = slug;
            }

            traktShowDto.TraktShowAliasDtos = new List<TraktShowAliasDto>();
            foreach (var existingShowTraktShowAlias in existingShow.TraktShowAliases)
            {
                var myShowAlias = new TraktShowAliasDto();
                myShowAlias.IdValue = existingShowTraktShowAlias.idValue;
                myShowAlias.IdType = existingShowTraktShowAlias.idType;
                traktShowDto.TraktShowAliasDtos.Add(myShowAlias);
            }

            foreach (var traktShowCreatedAlias in traktShowCreateDto.TraktShowCreatedAliases)
            {
                var found = false;
                foreach (var traktShowAliasDto in traktShowDto.TraktShowAliasDtos )
                {
                    if ((traktShowAliasDto.IdType == traktShowCreatedAlias.idType) &&
                        (traktShowAliasDto.IdValue == traktShowCreatedAlias.idValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    var newAlias = new TraktShowAliasDto();
                    newAlias.IdType = traktShowCreatedAlias.idType;
                    newAlias.IdValue = traktShowCreatedAlias.idValue;
                    traktShowDto.TraktShowAliasDtos.Add(newAlias);
                }
            }
            return traktShowDto;
        }

        private async Task UpdateAsync(TraktShowDto traktShowDto)
        {
            var existingShow = await _showRepository.GetAsync(traktShowDto.Id);
            if (existingShow != null)
            {
                foreach (var showAliasDto in traktShowDto.TraktShowAliasDtos)
                {
                    var found = false;
                    foreach (var existingAlias in existingShow.TraktShowAliases)
                    {
                        if (showAliasDto.IdType == existingAlias.idType)
                        {
                            if (showAliasDto.IdValue != existingAlias.idValue)
                            {
                                found = true;
                            }
                        }
                    }

                    if (found == false)
                    {
                        var traktShowAlias = new TraktShowAlias();
                        /*
                        existingShow.TraktShowAliases.Add(traktShowAlias);
                        var updatedShowDto = new TraktShowDto();
                        updatedShowDto.Id = existingShow.Id;
                        updatedShowDto.Name = existingShow.Name;
                        updatedShowDto.FirstAiredYear = existingShow.FirstAiredYear;
                        updatedShowDto.Slug = existingShow.Slug;
                        foreach (var existShowAlias in existingShow.TraktShowAliases)
                        {
                            var newAlias = new TraktShowAliasDto();
                            newAlias.IdType = existShowAlias.idType;
                            newAlias.IdValue = existShowAlias.idValue;
                            updatedShowDto.TraktShowAliasDtos.Add(newAlias);
                        }
                     
                        await UpdateTraktShowAsync(updatedShowDto);
                        */
                    }
                }
            }
        }

        public async Task SendCreateEvent(TraktShow show)
        {
            // Publish Trakt Show create event
            _logger.LogInformation("Sending TraktShowCreated Event: "  );
            var traktShowCreatedEto = new TraktShowCreatedEto();

            traktShowCreatedEto.TraktId = show.Id.ToString();
            traktShowCreatedEto.Name = show.Name;
            traktShowCreatedEto.FirstAiredYear = show.FirstAiredYear;
            traktShowCreatedEto.Slug = show.Slug;
            traktShowCreatedEto.TraktShowCreatedAliases = new List<(string idType, string idValue)>();
            
            foreach (var showAlias in show.TraktShowAliases)
            {
                traktShowCreatedEto.TraktShowCreatedAliases.Add((showAlias.idType, showAlias.idValue));
            }
            
            await _distributedEventBus.PublishAsync(traktShowCreatedEto);
        }
        
        public async Task ResendEvent(TraktShow show)
        {
            await SendCreateEvent(show);
        }

        public async Task UpdateTraktShowStatusAsync(TraktShowDto updatedTraktShow, FileStatus accepted)
        {
            throw new System.NotImplementedException();
        }
    }
}
