using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class EfCoreSeriesAliasRepository : EfCoreRepository<VideoServiceDbContext, SeriesAlias, Guid>, ISeriesAliasRepository
{
    
    public EfCoreSeriesAliasRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<SeriesAlias> FindByTypeValue(string idType, string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAliasList = await dbSet
                .Where(e => e.IdValue == idValue )
                .ToListAsync();
            return seriesAliasList[0];
        }
        catch 
        {
            return null;
        }
    }

    public async Task<SeriesAlias> GetByIdValue(string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAliasList = await dbSet
                .Where(e => e.IdValue == idValue )
                .ToListAsync();
            if (seriesAliasList.Count > 0)
            {
                return seriesAliasList[0];
            }
            else
            {
                return null;
            }
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<SeriesAlias>> GetByIdType(string idType)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAliasList = await dbSet
                .Where(e => e.IdType == idType )
                .ToListAsync();
            return seriesAliasList;
        }
        catch 
        {
            return null;
        }
    }

    public async Task<SeriesAlias> FindBySeriesIdType(Guid myLink, string idType)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAliasList = await dbSet
                .Where(e => e.SeriesId == myLink && 
                            e.IdType == idType)
                .ToListAsync();
            return seriesAliasList[0];
        }
        catch 
        {
            return null;
        }
    }

    public async Task<SeriesAlias> FindBySeriesTypeValueAsync(Guid seriesId, 
        string idType, string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAlias = await dbSet
                .Where(e => e.SeriesId == seriesId && 
                            e.IdType == idType &&
                            e.IdValue == idValue)
                .FirstAsync();
            return seriesAlias;
        }
        catch 
        {
            return null;
        }
    }

    public async Task<SeriesAlias> GetBySeriesType(Guid id, string type)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var seriesAlias = await dbSet
                .Where(e => e.SeriesId == id && 
                            e.IdType == type )
                .FirstAsync();
            return seriesAlias;
        }
        catch
        {
            return null;
        }
    }
}