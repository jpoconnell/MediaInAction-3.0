using System;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.UserPlayedMoviesNs;

public interface IUserPlayedMovieRepository :  IRepository<UserPlayedMovie, Guid>
{


}