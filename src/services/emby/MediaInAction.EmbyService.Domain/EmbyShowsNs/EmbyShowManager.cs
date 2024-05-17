using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowAliasNs;
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

    public async Task<EmbyShow> CreateAsync(
        string name,
        int year,
        List<( string idType, string idValue)>
            embyShowAliases
    )
    {
        // Create new embyShow
        var show = new EmbyShow()
        {
            Name = name,
            FirstAiredYear = year
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
