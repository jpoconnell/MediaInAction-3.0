using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyShowNs
{
    public interface IEmbyShowAppService : IApplicationService
    {
        Task<EmbyShowDto> GetAsync(Guid id);

        Task<PagedResultDto<EmbyShowDto>> GetListAsync(EmbyGetShowListDto input);

        Task UpdateAsync(Guid id, EmbyShowCreateDto input);
        
        Task<EmbyShowDto> CreateAsync( EmbyShowCreateDto input);
        Task DeleteAsync(Guid id);
        EmbyShowDto GetByShowNameYear(string show, int year);
    }
}