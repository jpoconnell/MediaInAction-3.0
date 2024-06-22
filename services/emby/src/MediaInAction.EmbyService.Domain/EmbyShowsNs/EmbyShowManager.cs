using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowAliasesNs.Dtos;
using MediaInAction.EmbyService.EmbyShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public class EmbyShowManager : DomainService
{
    private readonly IEmbyShowRepository _embyShowRepository;
    private ILogger<EmbyShowManager> _logger;
    
    public EmbyShowManager(
        IEmbyShowRepository embyShowRepository,
        ILogger<EmbyShowManager> logger
    )
    {
        _embyShowRepository = embyShowRepository;
        _logger = logger;
    }

    public async Task<EmbyShow> CreateAsync(EmbyShowCreateDto input)
    {
        // Create new embyShow
        var show = new EmbyShow()
        {
            Name = input.Name,
            FirstAiredYear = input.FirstAiredYear
        };

        show.ShowAliases = new List<EmbyShowAlias>();
        
        try
        {
            var createdEmbyShow = await _embyShowRepository.InsertAsync(show, true);
            return createdEmbyShow;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
