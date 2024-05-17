using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public interface IToBeMappedService
{
    Task CreateToBeMappedASync(string alias);
    Task<List<ToBeMappedDto>> GetNotProcessed();
    Task UpdateAsync(ToBeMappedDto toBeMapped);
}