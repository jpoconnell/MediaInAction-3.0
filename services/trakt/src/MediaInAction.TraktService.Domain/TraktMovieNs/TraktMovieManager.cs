using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieManager : DomainService
    {
        private readonly ITraktMovieRepository _movieRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private ILogger<TraktMovieManager> _logger;
        
        public TraktMovieManager(
            ITraktMovieRepository traktMovieRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TraktMovieManager> logger
        )
        {
            _movieRepository = traktMovieRepository;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }

        public async Task<TraktMovieDto> CreateAsync(TraktMovieCreateDto traktMovieCreateDto)
        {
            var existingMovie = new TraktMovie();
            if (traktMovieCreateDto.Slug != null)
            {
                 existingMovie = await _movieRepository.FirstOrDefaultAsync(p => p.Slug == traktMovieCreateDto.Slug);
            }
            else if (traktMovieCreateDto.Name != null)
            {
                 existingMovie = await _movieRepository.FirstOrDefaultAsync(p => p.Name == traktMovieCreateDto.Name);
            }
            else
            {
                return null;
            }
            if (existingMovie != null)
            {
                var updateMovie = new TraktMovieDto();
                updateMovie.Name = traktMovieCreateDto.Name;
                var updatedMovie  = await UpdateAsync(updateMovie);
                return updatedMovie;
            }
            else
            {
                var newTraktMovie = new TraktMovie(
                    GuidGenerator.Create(),
                    traktMovieCreateDto.Name,
                    traktMovieCreateDto.FirstAiredYear,
                    traktMovieCreateDto.TraktMovieCreateAliases,
                    traktMovieCreateDto.Slug
                );
                await _movieRepository.InsertAsync(newTraktMovie);
                var createdMovie = MapToDto(newTraktMovie);
              
                return createdMovie;
            }
        }

        public async Task<TraktMovieDto> UpdateAsync(TraktMovieDto traktMovieDto)
        {
            var existingMovie = await _movieRepository.FirstOrDefaultAsync(p => p.Slug == traktMovieDto.Slug);
            
            if (existingMovie.TraktMovieAliases.Count != traktMovieDto.TraktMovieAliasDtos.Count)
            {
                //TODO: update aliases
                
            }
            if (existingMovie.FirstAiredYear != traktMovieDto.FirstAiredYear)
            {
                existingMovie.FirstAiredYear = traktMovieDto.FirstAiredYear;
            }
            if (existingMovie.Name != traktMovieDto.Name)
            {
                existingMovie.Name = traktMovieDto.Name;
            }
            var updateMovie = new TraktMovieDto();
            // TODO: send update event
          
            var traktMovie = await _movieRepository.UpdateAsync(existingMovie, true);
            var updatedTraktMovie = MapToDto(traktMovie);
            await SendUpdateEvent(updatedTraktMovie);
            return updatedTraktMovie;
        }

        private TraktMovieDto MapToDto(TraktMovie updatedTraktMovie)
        {
            var traktMovieAliasDtos = updatedTraktMovie.TraktMovieAliases;
            
            return new TraktMovieDto
            {
                Id = updatedTraktMovie.Id,
                Name = updatedTraktMovie.Name,
                FirstAiredYear = updatedTraktMovie.FirstAiredYear,
                TraktMovieAliasDtos = traktMovieAliasDtos               
            };
        }

        private async Task SendUpdateEvent(TraktMovieDto updatedMovieDto)
        {
            // Publish Trakt Movie create event
            _logger.LogInformation("Sending TraktMovieUpdated Event: "  );
            /*
            await _distributedEventBus.PublishAsync(new TraktMovieUpdatedEto
            {
                TraktId = updatedMovie.Id.ToString() ,
                Slug = updatedMovie.Slug ,
                Name = updatedMovie.Name,
                FirstAiredYear = updatedMovie.FirstAiredYear,
                TraktMovieAliasDtos = updatedMovie.TraktMovieAliasDtos
            });
            */
        }

        public async Task UpdateTraktMovieStatusAsync(TraktMovieDto dbMovie, FileStatus status)
        {
            dbMovie.MovieStatus = status;
            await UpdateAsync(dbMovie);
        }
        
    }
}
