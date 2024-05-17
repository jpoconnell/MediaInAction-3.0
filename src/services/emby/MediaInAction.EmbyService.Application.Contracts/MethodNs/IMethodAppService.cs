using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.MethodNs;

public interface IMethodAppService : IApplicationService
{
    Task<List<Method>> GetListAsync();
}
