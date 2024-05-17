using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public class ToBeMappedService: IToBeMappedService
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;
    private readonly ILogger<ToBeMappedService> _logger;
    public ToBeMappedService(
        IToBeMappedRepository toBeMappedRepository,
        ILogger<ToBeMappedService> logger,
        ToBeMappedManager toBeMappedManager)
    {
        _toBeMappedRepository = toBeMappedRepository;
        _toBeMappedManager = toBeMappedManager;
        _logger = logger;
    }

    public async Task CreateToBeMappedASync(string alias)
    {
        await _toBeMappedManager.CreateToBeMappedAsync(alias);
    }

    public async Task<List<ToBeMappedDto>> GetNotProcessed()
    {
        var toBeMappeds = await _toBeMappedRepository.GetNotProcessed();
        var toBeMappedDtos = new List<ToBeMappedDto>();

        foreach (var toBeMapped in toBeMappeds)
        {
            var toBeMappedDto = new ToBeMappedDto
            {
                Alias = toBeMapped.Alias,
                Processed = toBeMapped.Processed,
                Id = toBeMapped.Id,
                Tries = toBeMapped.Tries
            };
            toBeMappedDtos.Add(toBeMappedDto);
        }

        return toBeMappedDtos;
    }

    public async Task UpdateAsync(ToBeMappedDto toBeMapped)
    {
        try
        {
            var toUpdate = await _toBeMappedRepository.GetAsync(toBeMapped.Id);
            toUpdate.Processed = toBeMapped.Processed;
            toUpdate.Tries = toBeMapped.Tries;
            await _toBeMappedRepository.UpdateAsync(toUpdate, true);
            _logger.LogInformation("Updated:" + toUpdate.Alias);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("ToBeMappedService.UpdateAsync:" + ex.Message);
        }
    }
}

