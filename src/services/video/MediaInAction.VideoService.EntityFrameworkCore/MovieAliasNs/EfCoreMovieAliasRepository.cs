using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.MovieAliasNs;

public class EfCoreMovieAliasRepository : EfCoreRepository<VideoServiceDbContext, MovieAlias, Guid>, IMovieAliasRepository
{
    
    public EfCoreMovieAliasRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<MovieAlias> FindByTypeValue(string idType, string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAliasList = await dbSet
                .Where(e => e.IdValue == idValue )
                .ToListAsync();
            return movieAliasList[0];
        }
        catch 
        {
            return null;
        }
    }

    public async Task<MovieAlias> GetByIdValue(string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAliasList = await dbSet
                .Where(e => e.IdValue == idValue )
                .ToListAsync();
            if (movieAliasList.Count > 0)
            {
                return movieAliasList[0];
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

    public async Task<List<MovieAlias>> GetByIdType(string idType)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAliasList = await dbSet
                .Where(e => e.IdType == idType )
                .ToListAsync();
            return movieAliasList;
        }
        catch 
        {
            return null;
        }
    }

    public async Task<MovieAlias> FindByMovieIdType(Guid myLink, string idType)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAliasList = await dbSet
                .Where(e => e.MovieId == myLink && 
                            e.IdType == idType)
                .ToListAsync();
            return movieAliasList[0];
        }
        catch
        {
            return null;
        }
    }

    public async Task<MovieAlias> FindByMovieTypeValueAsync(Guid movieId, 
        string idType, string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAlias = await dbSet
                .Where(e => e.MovieId == movieId && 
                            e.IdType == idType &&
                            e.IdValue == idValue)
                .FirstAsync();
            return movieAlias;
        }
        catch
        {
            return null;
        }
    }

    public async Task<MovieAlias> GetByMovieType(Guid id, string type)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            var movieAlias = await dbSet
                .Where(e => e.MovieId == id && 
                            e.IdType == type )
                .FirstAsync();
            return movieAlias;
        }
        catch
        {
            return null;
        }
        
    }
}