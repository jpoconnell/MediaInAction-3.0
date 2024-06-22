using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDb;
using MediaInAction.VideoService.EpisodeNs;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs;

public class MongoDbTraktShowRepository
    : MongoDbRepository<TraktServiceMongoDbContext, TraktShow, Guid>,
    ITraktShowRepository
{
    public MongoDbTraktShowRepository(
        IMongoDbContextProvider<TraktServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<TraktShow> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name);
    }
    
    public async Task<TraktShow> GetByTraktShowNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name && show.FirstAiredYear == year);
    }

    public async Task<TraktShow> GetBySlug(string showSlug, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .FirstOrDefaultAsync(show => show.Slug == showSlug );
    }

    public async Task<List<TraktShow>> GetListAllPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktShow>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TraktShow>> GetListPagedAsync(
        ISpecification<TraktShow> spec,
        int skipCount,
        int maxResultCount,
        string sorting = "Name",
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .OrderBy(sorting)
                .As<IMongoQueryable<TraktShow>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    
    public async Task<List<TraktShow>> GetDashboardAsync(ISpecification<TraktShow> specification)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .As<IMongoQueryable<TraktShow>>()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<TraktShow>> GetActiveListAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .As<IMongoQueryable<TraktShow>>()
            .Where(a => a.IsActive == true)
            .ToListAsync();
    }

    public async Task<List<TraktShow>> GetTraktShowBySpec(ISpecification<TraktShow> spec, 
        bool includeDetails = false)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .As<IMongoQueryable<TraktShow>>()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    
    public async Task<TraktShow> GetBySlug(string showSlug)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Slug == showSlug );
    }
}
