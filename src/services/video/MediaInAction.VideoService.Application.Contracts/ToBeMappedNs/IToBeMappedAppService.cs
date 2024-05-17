using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.ToBeMappedNs;
public interface IToBeMappedAppService : IApplicationService
{
    Task<ToBeMappedDto> GetAsync(Guid id);
    Task<ToBeMappedDto> CreateAsync(ToBeMappedCreateDto input);

    Task<List<ToBeMappedDto>> GetToBeMappedsAsync(GetToBeMappedsInput input);
    Task<ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input);
    Task<PagedResultDto<ToBeMappedDto>> GetListPagedAsync(GetToBeMappedsInput input);
}
