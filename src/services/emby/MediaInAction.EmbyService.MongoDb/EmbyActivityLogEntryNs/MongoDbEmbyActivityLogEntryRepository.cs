using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MediaInAction.EmbyService.ActivityLogEntryNs;
using MediaInAction.EmbyService.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace MediaInAction.EmbyService.EmbyActivityLogEntryNs;

public class MongoDbEmbyActivityLogEntryRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyActivityLogEntry, Guid>,
    IEmbyActivityLogEntryRepository
{
    public MongoDbEmbyActivityLogEntryRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<List<EmbyActivityLogEntry>> GetListPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<EmbyActivityLogEntry>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<EmbyActivityLogEntry>> GetListAsync()
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .OrderBy("Date")
                .As<IMongoQueryable<EmbyActivityLogEntry>>()
                .ToListAsync();
        }
        catch
        {
            return null;
        }
    }

    public async Task<EmbyActivityLogEntry> FindByExternalId(long externalId)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.ExternalId == externalId);

    }
}
