using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.SeriesNs.Specifications;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public class EfCoreSeriesRepository : EfCoreRepository<VideoServiceDbContext, Series, Guid>, ISeriesRepository
{
    public EfCoreSeriesRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public override async Task<IQueryable<Series>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .IncludeDetails();
    }
    
    public async Task<Series> FindBySeriesNameYear(string name, 
        int firstAiredYear,   
        bool includeDetails = false)
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

    public async Task<List<Series>> GetActiveList()
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

    public async Task<List<Series>> GetSeriesCollectionAsync(
        ISpecification<Series> spec,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .IncludeDetails(includeDetails)
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async Task<List<Series>> GetListPagedAsync(
        ISpecification<Series> spec,
        int skipCount,
        int maxResultCount,
        string sorting = "Name",
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .OrderBy( "Name")
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<Series>> GetNoDefault()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            
            var allSeries = await dbSet
                .IncludeDetails(true)
                .ToListAsync();

            return allSeries;
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Series>> GetSeriesAsync(
        ISpecification<Series> spec, 
        bool b)
    {
        CancellationToken cancellationToken = default;
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Series>> GetSeriessByUserId(Guid getId, 
        ISpecification<Series> spec, 
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .IncludeDetails(includeDetails)
            .Where(spec.ToExpression())
            .OrderByDescending(o => o.Name)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Series>> GetSeriesBySpec(
        ISpecification<Series> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await (await GetDbSetAsync())
                .Where(spec.ToExpression())
                .IncludeDetails(includeDetails)
                .OrderBy("Name")
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<Series>> GetDashboardAsync(
        ISpecification<Series> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await (await GetDbSetAsync())
                .Where(spec.ToExpression())
                .IncludeDetails(includeDetails)
                .OrderBy("Name")
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        
    }

    public async Task<List<Series>> GetBySeriesName(
        string seriesName, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .IncludeDetails(true)
                .Where(e => e.Name == seriesName )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }
}