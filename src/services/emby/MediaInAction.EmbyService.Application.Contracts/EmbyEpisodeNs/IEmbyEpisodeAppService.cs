using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public interface IEmbyEpisodeAppService : IApplicationService
{
    Task<EmbyEpisodeDto> GetAsync(Guid id);

    Task<PagedResultDto<EmbyEpisodeDto>> GetListAsync(GetEmbyEpisodeListDto input);

    Task UpdateAsync(Guid id, EmbyEpisodeCreateDto input);

    Task DeleteAsync(Guid id);
    Task<EmbyEpisodeDto> CreateAsync( EmbyEpisodeCreateDto input);
    Task<EmbyEpisodeDto> GetEpisodeAsync(GetEmbyEpisodeDto getEpisodeInput);
}