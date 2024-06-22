using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs;

public class EfCoreToBeMappedRepository : EfCoreRepository<VideoServiceDbContext, ToBeMapped, Guid>, IToBeMappedRepository
{
    public EfCoreToBeMappedRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }
    
    public async Task<List<ToBeMapped>> GetAllListAsync()
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

    public async Task<ToBeMapped> FindByAlias(string alias)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Alias == alias )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<ToBeMapped>> GetNotProcessed()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Processed == false )
                .OrderByDescending(d => d.Alias)
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<ToBeMapped>> GetToBeMappedsByUserId(Guid getId, 
        ISpecification<ToBeMapped> spec, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .OrderByDescending(o => o.Alias)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<ToBeMapped>> GetListPagedAsync(ISpecification<ToBeMapped> spec,
        int inputSkipCount, 
        int inputMaxResultCount, string empty, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .OrderByDescending(o => o.Alias)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}