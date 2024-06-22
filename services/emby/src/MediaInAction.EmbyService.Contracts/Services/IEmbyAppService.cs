using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.Services;

public interface IEmbyAppService : IApplicationService
{
    Task<EmbyDto> GetAsync(Guid? anonymousUserId);
    Task<EmbyDto> AddProductAsync(AddProductDto input);
    Task<EmbyDto> RemoveProductAsync(RemoveProductDto input);
}