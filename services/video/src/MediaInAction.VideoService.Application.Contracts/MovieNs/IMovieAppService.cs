using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.MovieNs;

public interface IMovieAppService : IApplicationService
{
    Task<MovieDto> GetAsync(Guid id);
    Task<MovieDto> CreateAsync(MovieCreateDto input);
    Task<List<MovieDto>> GetMoviesAsync(GetMoviesInput input);
    Task<PagedResultDto<MovieDto>> GetListPagedAsync(GetMoviesInput input);
    Task<MovieDto> GetByMovieNameAsync(string name);
    Task SetAsInActiveAsync(Guid id);
    Task<DashboardDto> GetDashboardAsync(DashboardInput input);
    Task<MovieDto> GetMovieAsync(string newMovieName, int newMovieFirstAiredYear);
}
