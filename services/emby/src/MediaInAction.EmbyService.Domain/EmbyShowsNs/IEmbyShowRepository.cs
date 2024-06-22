using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public interface IEmbyShowRepository :  IRepository<EmbyShow, Guid>
{
    Task<EmbyShow> GetByServerNameAsync(string server,string folder,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<EmbyShow> GetByNameAsync(string showName);
    Task<EmbyContent> GetByEmbyId(string tvShowId);
    Task DeleteTv(EmbyContent existingTv);
    Task AddRange(HashSet<EmbyContent> mediaToAdd);
    Task<List<EmbyShow>> GetListPagedAsync(
        ISpecification<EmbyShow> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting);
}