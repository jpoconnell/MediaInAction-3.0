using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs;

public interface IFileEntryRepository : IRepository<FileEntry, Guid>
{
    Task<List<FileEntry>> GetFileEntriesAsync(
        ISpecification<FileEntry> spec,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<List<FileEntry>> GetDashboardAsync(
        ISpecification<FileEntry> spec,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
    
    Task<List<FileEntry>>GetBySpec();
    
    Task<FileEntry> GetByExternalId(Guid fileId);
    Task<FileEntry> FindFileEntry(string server, 
        string directory, 
        string fileName, 
        ListType listName);
    
    Task<List<FileEntry>> GetListPagedAsync(
        ISpecification<FileEntry> spec,
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
}