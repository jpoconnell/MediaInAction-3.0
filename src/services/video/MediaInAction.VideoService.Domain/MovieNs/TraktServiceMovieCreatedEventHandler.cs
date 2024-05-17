using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.VideoService.MovieAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.MovieNs;

public class TraktServiceMovieCreatedEventHandler : IDistributedEventHandler<TraktMovieCreatedEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceMovieCreatedEventHandler> _logger;
    private readonly MovieManager _movieManager;
    
    public TraktServiceMovieCreatedEventHandler(
        IDistributedEventBus eventBus,
        MovieManager movieManager,
        ILogger<TraktServiceMovieCreatedEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _movieManager = movieManager;
    }

    public async Task HandleEventAsync(TraktMovieCreatedEto eventData)
    {
        if (eventData.TraktMovieCreatedAliases.Count == 0)
        {
            return;
        }
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktMovieIdNotGuid);
        }
        
        var movieAliasList = new List<MovieAlias>();
        
        var acceptedMovie = await _movieManager.AcceptTraktMovieAsync(eventData.TraktId,
            eventData.Slug, eventData.Name, eventData.FirstAiredYear, movieAliasList);

        _logger.LogInformation("Sending Trakt Movie Accepted Event");
        await _eventBus.PublishAsync(new TraktMovieAcceptedEto
        {
            TraktId = acceptedMovie.Id.ToString(),
            Slug = eventData.Slug,
            Name = eventData.Name,
            Year = eventData.FirstAiredYear
        });
    }
}