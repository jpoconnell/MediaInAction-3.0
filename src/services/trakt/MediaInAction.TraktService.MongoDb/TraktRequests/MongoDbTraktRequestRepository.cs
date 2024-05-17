using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktRequests;

public class MongoDbTraktRequestRepository
    : MongoDbRepository<TraktServiceMongoDbContext, TraktRequest, Guid>,
    ITraktRequestRepository
{
    public MongoDbTraktRequestRepository(
        IMongoDbContextProvider<TraktServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<TraktRequest> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(author => author.Command == name);
    }

    public async Task<List<TraktRequest>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<TraktRequest, IMongoQueryable<TraktRequest>>(
                !filter.IsNullOrWhiteSpace(),
                author => author.Command.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktRequest>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public Task<List<TraktRequest>> GetTraktShowsByUserId(
        Guid userId, 
        ISpecification<TraktRequest> spec, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TraktRequest>> GetTraktShowsAsync(
        ISpecification<TraktRequest> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<TraktRequest>> GetDashboardAsync(
        ISpecification<TraktRequest> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TraktRequest> GetByTraktShowNameYearAsync(
        string name, 
        int year, 
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
