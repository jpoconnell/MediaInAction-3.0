using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMovieNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyMovieNs
{
    public interface IEmbyMovieAppService : IApplicationService
    {
        Task<EmbyMovieDto> GetAsync(Guid id);

        Task<PagedResultDto<EmbyMovieDto>> GetListAsync(GetEmbyMovieListDto input);

        Task UpdateAsync(Guid id, EmbyMovieCreateDto input);

        Task DeleteAsync(Guid id);
        Task<EmbyMovieDto> CreateAsync( EmbyMovieCreateDto input);
        Task<EmbyMovieDto> GetMovieAsync(string name, int year);
    }
}