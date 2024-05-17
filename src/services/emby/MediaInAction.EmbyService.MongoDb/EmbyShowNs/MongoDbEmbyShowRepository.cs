using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using MediaInAction.EmbyService.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class MongoDbEmbyShowRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyShow, Guid>,
    IEmbyShowRepository
{
    public MongoDbEmbyShowRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<EmbyShow> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name);
    }

    public async Task<List<EmbyShow>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<EmbyShow, IMongoQueryable<EmbyShow>>(
                !filter.IsNullOrWhiteSpace(),
                show => show.Name.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<EmbyShow>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }


    public async Task<EmbyShow> GetByEmbyShowNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name && show.FirstAiredYear == year);
    }

    public async Task<EmbyShow> GetBySlug(string showSlug, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .FirstOrDefaultAsync(show => show.Slug == showSlug );

    }

    public Task<List<EmbyShow>> GetListPagedAsync(
        ISpecification<EmbyShow> spec, 
        int skipCount, int maxResultCount, 
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<EmbyShow> GetBySlug(string showSlug)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Slug == showSlug );
    }

    public Task<EmbyShow> GetByServerNameAsync(
        string server, 
        string folder, 
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyShow> GetByNameAsync(string seriesName)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyContent> GetByEmbyId(string tvShowId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTv(EmbyContent existingTv)
    {
        throw new NotImplementedException();
    }

    public Task AddRange(HashSet<EmbyContent> mediaToAdd)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyShow>> GetListPagedAsync(
        ISpecification<EmbyShow> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting)
    {
        throw new NotImplementedException();
    }
}
