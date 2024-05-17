using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class EmbyShowLibService : IEmbyShowLibService
{
    private readonly ILogger _logger;
    private readonly EmbyShowManager _embyShowManager;
    private readonly IEmbyShowRepository _embyShowRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public EmbyShowLibService(
        IEmbyShowRepository embyShowRepository,
        EmbyShowManager embyShowManager,
        IDistributedEventBus distributedEventBus
    )
    {
        _logger = NullLogger.Instance;
        _embyShowRepository = embyShowRepository;
        _embyShowManager = embyShowManager;
        _distributedEventBus = distributedEventBus;
    }

    public async Task UpdateAddFromDto(EmbyShowDto show)
    {
        try 
        { 
            await CreateUpdateFolder(show);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }

    public async Task<List<EmbyShowDto>> GetAll()
    {
       var showList = await _embyShowRepository.GetListAsync();
       var returnList = new List<EmbyShowDto>();
       foreach (var embyShow in showList)
       {
           var show = new EmbyShowDto
           {
               Name = embyShow.Name,
               EmbyId = embyShow.EmbyId
           };
           returnList.Add(show);
       }

       return returnList;
    }

    public async Task SendAllShowsEventList()
    {
        var embyShowListEtos = new List<EmbyShowCreatedEto>();
            
        var embyShows = await _embyShowRepository.GetListAsync(true);
        foreach (var show in embyShows)
        {
            var embyShow = new EmbyShowCreatedEto();
            embyShow.EmbyId = show.EmbyId.ToString();
            embyShow.Name = show.Name;
            embyShowListEtos.Add(embyShow);
        }

        foreach (var embyShowCreateEto in embyShowListEtos)
        {
            await _distributedEventBus.PublishAsync(embyShowCreateEto);
        }
    }
    
    private async Task CreateUpdateFolder(EmbyShowDto show)
    {
        try
        {
            var embyShowAliases = new List<( string idType, string idValue)>();
            var dbShow = await _embyShowRepository.GetByNameAsync(show.Name);
            if (dbShow == null)
            {
                var createdFolder = await _embyShowManager.CreateAsync(show.Name, 
                    show.Year, embyShowAliases);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
}

