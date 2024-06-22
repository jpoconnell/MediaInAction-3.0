using System.Threading.Tasks;
using MediaInAction.TraktService.TraktRequests;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.TraktMethods;

public interface ITraktMethod : ITransientDependency
{
    string Name { get; }

    public Task<TraktRequestStartResultDto> StartAsync(TraktRequest traktRequest, TraktRequestStartDto input);

    public Task<TraktRequest> CompleteAsync(ITraktRequestRepository traktRequestRepository, string token);

    public Task HandleWebhookAsync(string payload);
}