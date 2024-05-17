using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktMethods;

public interface ITraktMethodAppService : IApplicationService
{
    Task<List<TraktMethodDto>> GetListAsync();
}
