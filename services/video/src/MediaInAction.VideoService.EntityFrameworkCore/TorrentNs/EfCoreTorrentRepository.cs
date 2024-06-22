using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs;

public class EfCoreTorrentRepository : EfCoreRepository<VideoServiceDbContext, Torrent, Guid>, ITorrentRepository
{
    public EfCoreTorrentRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }
    
    public async Task<List<Torrent>> GetAllListAsync()
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

    public async Task<Torrent> FindByHash(string hash)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Hash == hash )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<Torrent> FindByName(string name)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Name == name )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Torrent>> GetMapped(bool isMapped)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IsMapped == false )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }
    
    public async Task<List<Torrent>> GetTorrentStatus(FileStatus status)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.TorrentStatus == status )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Torrent>> GetListPagedAsync(ISpecification<Torrent> specification, 
        int skipCount, 
        int maxResultCount, 
        string empty,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await (await GetDbSetAsync())
                
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

    private object await(DbSet<Torrent> getDbSetAsync)
    {
        throw new NotImplementedException();
    }
}