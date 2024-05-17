using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyJobs
{
    public interface IEmbyJobService : IApplicationService
    {
        Task SendAll();
    }
}
