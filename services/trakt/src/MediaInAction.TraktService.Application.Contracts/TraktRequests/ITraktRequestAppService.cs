using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktRequests;

public interface ITraktRequestAppService : IApplicationService
{
    Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input);

    Task<TraktRequestStartResultDto> StartAsync(string paymentMethod, TraktRequestStartDto input);

    Task<TraktRequestDto> CompleteAsync(string paymentMethod, TraktRequestCompleteInputDto input);

    Task<bool> HandleWebhookAsync(string paymentMethod, string payload);
}
