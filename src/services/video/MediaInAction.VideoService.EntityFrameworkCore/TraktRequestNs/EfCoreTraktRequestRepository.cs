using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.TraktRequestNs;

public class EfCoreTraktRequestRepository : EfCoreRepository<VideoServiceDbContext, TraktRequest, Guid>, ITraktRequestRepository
{
    public EfCoreTraktRequestRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<List<TraktRequest>> GetUnCompleteRequests()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IsComplete == false )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }
}