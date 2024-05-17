using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDb;
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

    public Task<List<TraktShow>> GetDashboardAsync(ISpecification<TraktShow> specification)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TraktShow>> GetActiveListAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .As<IMongoQueryable<TraktShow>>()
            .Where(a => a.IsActive == true)
            .ToListAsync();
    }

    public Task<List<TraktShow>> GetTraktShowBySpec(ISpecification<TraktShow> spec, bool b)
    {
        throw new NotImplementedException();
    }

    public async Task<TraktShow> GetBySlug(string showSlug)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Slug == showSlug );
    }
}
