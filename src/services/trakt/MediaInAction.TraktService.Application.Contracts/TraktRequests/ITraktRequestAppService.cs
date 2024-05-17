using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktRequests;

public interface ITraktRequestAppService : IApplicationService
{
    Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input);

    Task<TraktRequestStartResultDto> StartAsync(string traktMethod, TraktRequestStartDto input);

    Task<TraktRequestDto> CompleteAsync(string traktMethod, TraktRequestCompleteInputDto input);

    Task<bool> HandleWebhookAsync(string traktMethod, string payload);
}
