using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Models;
using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class EmbyMovieLibService : IEmbyMovieLibService
{
    private readonly ILogger<EmbyMovieLibService> _logger;
    private readonly EmbyMovieManager _embyMovieManager;
    private readonly IEmbyMovieRepository _embyMovieRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public EmbyMovieLibService(
        IEmbyMovieRepository  embyMovieRepository,
        EmbyMovieManager embyMovieManager,
        IDistributedEventBus distributedEventBus,
        ILogger<EmbyMovieLibService> logger
    )
    {
        _logger = logger;
        _embyMovieRepository = embyMovieRepository;
        _embyMovieManager = embyMovieManager;
        _distributedEventBus = distributedEventBus;
    }

    public async Task UpdateAddFromDto(EmbyMovieDto episode)
    {
        try 
        { 
            await CreateUpdateFolder(episode);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }

    public async Task SendAllMoviesEventList()
    {
        var embyMovieListEtos = new List<EmbyMovieCreatedEto>();

        var embyMovies = await _embyMovieRepository.GetListAsync(true);
        foreach (var show in embyMovies)
        {
            var embyMovie = new EmbyMovieCreatedEto();
            embyMovie.EmbyId = show.Id.ToString();
            embyMovie.Name = show.Name;
            embyMovieListEtos.Add(embyMovie);
        }

        foreach (var embyMovieCreateEto in embyMovieListEtos)
        {
            await _distributedEventBus.PublishAsync(embyMovieCreateEto);
        }
    }

    private async Task CreateUpdateFolder(EmbyMovieDto movie)
    {
        try
        {
            var sss = new List<( string idType, string idValue)>();
            var dbMovie = await _embyMovieRepository.GetByNameAsync(movie.Name);
            if (dbMovie == null)
            {
             //   var createdMovie = await _embyMovieManager.CreateAsync(
               //     movie.Name, movie., sss );
            }
           
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
}

