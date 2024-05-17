using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs
{
    public interface IToBeMappedRepository : IRepository<ToBeMapped, Guid>
    {
        Task<ToBeMapped> FindByAlias(string alias);
        Task<List<ToBeMapped>> GetNotProcessed();
        
        Task<List<ToBeMapped>> GetToBeMappedsByUserId(
            Guid userId,
            ISpecification<ToBeMapped> spec,
            CancellationToken cancellationToken = default);
        
        Task<List<ToBeMapped>> GetListPagedAsync(
            ISpecification<ToBeMapped> specification, 
            int inputSkipCount, 
            int inputMaxResultCount, 
            string empty,
            CancellationToken cancellationToken = default);
    }
}
