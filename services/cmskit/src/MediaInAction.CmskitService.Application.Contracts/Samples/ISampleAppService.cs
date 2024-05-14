using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.CmskitService.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
