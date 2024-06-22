using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs;

public class EfCoreFileEntryRepository : EfCoreRepository<VideoServiceDbContext, FileEntry, Guid>, IFileEntryRepository
{
    public EfCoreFileEntryRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<List<FileEntry>> GetFileEntriesByUserId(Guid userId, 
        ISpecification<FileEntry> spec, 
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));

    }

    public async Task<List<FileEntry>> GetFileEntriesAsync(
        ISpecification<FileEntry> spec, 
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));

    }

    public async Task<List<FileEntry>> GetDashboardAsync(
        ISpecification<FileEntry> spec, bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public Task<List<FileEntry>> GetBySpec()
    {
        throw new NotImplementedException();
    }

    public async Task<FileEntry> FindFileEntry(
        string server, 
        string directory, 
        string fileName, 
        ListType listName)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Server == server && 
                            e.Directory == directory && 
                            e.FileName == fileName &&
                            e.ListName == listName)
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<FileEntry>> GetListPagedAsync(
        ISpecification<FileEntry> spec, 
        int skipCount, 
        int maxResultCount, 
        string filename,
        bool includeDetails = false,  
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await (await GetDbSetAsync())
                .Where(spec.ToExpression())
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

    public async Task<List<FileEntry>> GetByLink(Guid link)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.EpisodeLink == link )
                .OrderBy(f => f.Sequence)
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<FileEntry>> GetUnMapped()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.FileStatus < FileStatus.Mapped )
                .OrderBy(f => f.Sequence)
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<FileEntry>> GetFileEntriesByUserId(Guid getId, 
        ISpecification<FileEntry> spec, 
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .OrderByDescending(o => o.FileName)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<FileEntry> GetFileEntry(string fileName, string server, string directory)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.FileName == fileName  && e.Server == server && e.Directory == directory)
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<FileEntry>> GetMapped()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.FileStatus > FileStatus.Accepted )
                .OrderBy(f => f.Sequence)
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<FileEntry> GetByExternalId(Guid fileId)
    {        
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.ExternalId == fileId.ToString())
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<FileEntry>> GetAllListAsync()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderBy(f => f.Sequence)
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }
}