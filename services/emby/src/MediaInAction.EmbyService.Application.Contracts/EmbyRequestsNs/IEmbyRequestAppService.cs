using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyRequestsNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService.EmbyRequestsNs
{
    public interface IEmbyRequestAppService : IApplicationService
    {
        //  Task<EmbyRequestDto> CreateAsync(EmbyRequestCreationDto input);

        Task<EmbyRequestStartResultDto> StartAsync(string traktMethod, EmbyRequestStartDto input);

        Task<EmbyRequestDto> CompleteAsync(string traktMethod, EmbyRequestCompleteInputDto input);

        Task<bool> HandleWebhookAsync(string traktMethod, string payload);
    }
}
