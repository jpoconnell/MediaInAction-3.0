using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace  MediaInAction.VideoService.MovieNs;

public interface IMovieRepository : IRepository<Movie, Guid>
{
    Task<List<Movie>> GetMoviesBySpec(
        ISpecification<Movie> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<Movie> GetByMovieNameAsync(string name);
    
    Task<List<Movie>> GetDashboardAsync(
        ISpecification<Movie> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
 
    Task<Movie> FindByMovieNameYear(string name, 
        int firstAiredYear,
        bool includeDetails = true);

    Task<List<Movie>> GetByMovieName(string movieName);
}
