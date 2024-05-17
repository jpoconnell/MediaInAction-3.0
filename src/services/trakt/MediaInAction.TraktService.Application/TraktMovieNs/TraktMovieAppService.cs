using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktMovieNs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs;

[Authorize(TraktServicePermissions.TraktMovie.Default)]
public class MovieAppService : TraktServiceAppService, ITraktMovieAppService
{
    private readonly ILogger<MovieAppService> _logger;
    private readonly ITraktMovieRepository _traktMovieRepository;
    private readonly TraktMovieManager _traktMovieManager;

    public MovieAppService(
        ITraktMovieRepository movieRepository,
        ILogger<MovieAppService>  logger,
        TraktMovieManager traktMovieManager)
    {
        _traktMovieRepository = movieRepository;
        _traktMovieManager = traktMovieManager;
        _logger = logger;
    }
    
   [AllowAnonymous]
    public async Task<MovieDashboardDto> GetDashboardAsync(MovieDashboardInput input)
    {
        return new MovieDashboardDto()
        {
            TraktMovieStatusDto = await GetCountOfTotalMovieStatusAsync(input.Filter),
        };
    }

    private async Task<List<TraktMovieStatusDto>> GetCountOfTotalMovieStatusAsync(string filter)
    {
        ISpecification<TraktMovie> specification = SpecificationFactory.Create(filter);
        var movies = await _traktMovieRepository.GetDashboardAsync(specification);
        return CreateMovieStatusDtoMapping(movies);
    }

    private List<TraktMovieStatusDto> CreateMovieStatusDtoMapping(List<TraktMovie> movies)
    {
        throw new System.NotImplementedException();
    }
}