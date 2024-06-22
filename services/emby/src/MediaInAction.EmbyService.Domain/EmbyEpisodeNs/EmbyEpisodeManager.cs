using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasNs;
using MediaInAction.EmbyService.EmbyEpisodesNs;
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

    public async Task<EmbyEpisode> CreateAsync(EmbyEpisodeCreateDto input)
    {
        try
        {
            // Create new episode
            var embyEpisode = new EmbyEpisode();
            //embyEpisode.SetId(GuidGenerator.Create());
            embyEpisode.ShowId = input.EmbySeriesId;
            embyEpisode.SeasonNum = input.SeasonNum;
            embyEpisode.EpisodeNum = input.EpisodeNum;
            embyEpisode.AiredDate = input.AiredDate;
            embyEpisode.EpisodeAliases = new List<EmbyEpisodeAlias>();

            foreach (var alias in input.EmbyEpisodeAliasCreateDtos)
            {
                embyEpisode.EpisodeAliases.Add(new EmbyEpisodeAlias
                {
                    IdType = alias.IdType,
                    IdValue = alias.IdValue
                });
            }
            var createdEpisode = await _embyEpisodeRepository.InsertAsync(embyEpisode);
            return createdEpisode;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
