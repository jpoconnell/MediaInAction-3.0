using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.DelugeService.MongoDb;
using MediaInAction.DelugeService.TorrentNs;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.TorrentsNs;

public class MongoDbTorrentRepository
    : MongoDbRepository<DelugeServiceMongoDbContext, Torrent, Guid>,
    ITorrentRepository
{
    public MongoDbTorrentRepository(
        IMongoDbContextProvider<DelugeServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<Torrent> FindByNameAsync(string name)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name.Contains(name));
    }

    public async Task<List<Torrent>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<Torrent, IMongoQueryable<Torrent>>(
                !filter.IsNullOrWhiteSpace(),
                show => show.Name.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<Torrent>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    

    public async Task<List<Torrent>> GetListPagedAsync(ISpecification<Torrent> spec, 
        int skipCount, int maxResultCount, 
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .Where(spec.ToExpression())
            .OrderBy(sorting)
            .As<IMongoQueryable<Torrent>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<Torrent>> GetByStatus(string status)
    {
        var queryable = await GetMongoQueryableAsync();
        return null;
    }

    public async Task<Torrent> GetByHashAsync(string hash)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(torrent => torrent.Hash == hash );
    }
}
