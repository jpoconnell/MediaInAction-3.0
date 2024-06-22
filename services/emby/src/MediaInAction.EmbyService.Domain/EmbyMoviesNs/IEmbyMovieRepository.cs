using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyMoviesNs;

public interface IEmbyMovieRepository :  IRepository<EmbyMovie, Guid>
{
    Task<EmbyMovie> GetByNameAsync(string episodeName);
}