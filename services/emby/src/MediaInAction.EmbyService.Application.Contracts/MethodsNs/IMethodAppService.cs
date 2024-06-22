using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.MethodsNs
{
    public interface IMethodAppService : IApplicationService
    {
        Task<List<Method>> GetListAsync();
    }
}
