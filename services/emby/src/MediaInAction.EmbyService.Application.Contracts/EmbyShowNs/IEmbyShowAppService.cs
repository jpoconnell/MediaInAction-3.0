using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EmbyShowCreateDto = MediaInAction.EmbyService.EmbyShowsNs.EmbyShowCreateDto;

namespace MediaInAction.EmbyService.EmbyShowNs
{
    public interface IEmbyShowAppService : IApplicationService
    {
        Task<EmbyShowDto> GetAsync(Guid id);

        Task<PagedResultDto<EmbyShowDto>> GetListAsync(EmbyGetShowListDto input);

        Task UpdateAsync(EmbyShowDto input);
        
        Task<EmbyShowDto> CreateAsync( EmbyShowCreateDto input);
        Task DeleteAsync(Guid id);
        EmbyShowDto GetByShowNameYear(string show, int year);
    }
}