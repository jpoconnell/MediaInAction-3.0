using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs;

public interface ITraktMovieRepository : IRepository<TraktMovie, Guid>
{
    Task<List<TraktMovie>> GetDashboardAsync(
        ISpecification<TraktMovie> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<TraktMovie> GetByMovieNameYearAsync(string name,int year);
    
    Task<TraktMovie> GetBySlug(string slug);
}