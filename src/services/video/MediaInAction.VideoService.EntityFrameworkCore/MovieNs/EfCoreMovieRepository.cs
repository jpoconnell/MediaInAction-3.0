using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs;

public class EfCoreMovieRepository : EfCoreRepository<VideoServiceDbContext, Movie, Guid>, IMovieRepository
{
    public EfCoreMovieRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public override async Task<IQueryable<Movie>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .IncludeDetails();
    }

    public async Task<List<Movie>> GetAllListAsync()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }
    
    public async Task<Movie> FindByMovieNameYear(string name, 
        int firstAiredYear, 
        bool includeDetails = true)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .IncludeDetails(includeDetails)
                .Where(e => e.Name == name && e.FirstAiredYear == firstAiredYear )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Movie>> GetByMovieName(string movieName)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Name == movieName  )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Movie>> GetActiveList()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IsActive == true  )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Movie>> GetMoviesBySpec(
        ISpecification<Movie> spec,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .IncludeDetails(includeDetails)
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async Task<Movie> GetByMovieNameAsync(string name)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Name == name  )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Movie>> GetDashboardAsync(ISpecification<Movie> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}