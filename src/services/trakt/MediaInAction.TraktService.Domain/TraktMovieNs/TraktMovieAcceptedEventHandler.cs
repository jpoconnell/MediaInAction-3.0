using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMovieAcceptedEventHandler : IDistributedEventHandler<TraktMovieAcceptedEto>, ITransientDependency
{
    private readonly ITraktMovieRepository _movieRepository;
    private readonly TraktMovieManager _movieManager;
    private readonly ILogger<TraktMovieAcceptedEventHandler> _logger;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public TraktMovieAcceptedEventHandler(
        ITraktMovieRepository movieRepository,
        ILogger<TraktMovieAcceptedEventHandler> logger,
        TraktMovieManager movieManager,
        IDistributedEventBus distributedEventBus)
    {
        _movieRepository = movieRepository;
        _movieManager = movieManager;
        _distributedEventBus = distributedEventBus;
        _logger = logger;
    }

    public async Task HandleEventAsync(TraktMovieAcceptedEto eventData)
    {
       // _logger.LogInformation("Got MovieAcceptedEto Event");
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            // try finding it by 
           var dbMovie = await _movieRepository.GetBySlug(eventData.Slug);
           if (dbMovie == null)
           {
               throw new BusinessException(TraktServiceDomainErrorCodes.MovieNotInDatabase);
           }
           else
           {
               var updatedMovie = new TraktMovieDto();
               updatedMovie.Name = eventData.Name;
               updatedMovie.Slug = eventData.Slug;
               updatedMovie.FirstAiredYear = eventData.Year;
               
               await _movieManager.UpdateTraktMovieStatusAsync(updatedMovie, FileStatus.Accepted);
               //_logger.LogInformation("Movie Status Updated");
           }
        }
        else
        {
            var movie = await _movieRepository.GetBySlug(eventData.Slug);
            if (movie == null)
            {
                throw new BusinessException(TraktServiceDomainErrorCodes.MovieIdNotInDatabase);
            }
            else
            {
                if (movie.TraktStatus != FileStatus.Accepted)
                {
                    movie.TraktStatus = FileStatus.Accepted;
                    await _movieRepository.UpdateAsync(movie, true);
                    await _distributedEventBus.PublishAsync(new TraktMovieAcknowledgeEto
                    {
                        TraktId = movie.Id.ToString() ,
                        Slug = movie.Slug ,
                        Name = movie.Name,
                        Year = movie.FirstAiredYear,
                    });
                }
            }
        }
    }
}
