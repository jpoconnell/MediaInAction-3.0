using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public interface IEmbyMovieRepository :  IRepository<EmbyMovie, Guid>
{
    Task<EmbyMovie> GetByServerNameAsync(string server,string folder,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<EmbyMovie> GetByNameAsync(string episodeName);
    Task UpdateRange(HashSet<EmbyContent> mediaToUpdate);
    Task AddRange(HashSet<EmbyContent> mediaToAdd);
    Task<EmbyContent> GetByEmbyId(string movieInfoId);
    Task<EmbyMovie> GetByMovieId(int theMovieDbId, Guid userId);
}