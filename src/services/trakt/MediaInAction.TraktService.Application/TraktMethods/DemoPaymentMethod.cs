using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktRequests;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.TraktMethods;

[ExposeServices(typeof(ITraktMethod), typeof(DemoTraktMethod))]
public class DemoTraktMethod : ITraktMethod
{
    public string Name => TraktMethodNames.UpdateWatched;

    public Task<TraktRequestStartResultDto> StartAsync(TraktRequest traktRequest, TraktRequestStartDto input)
    {
        /*
        if (traktRequest.Products.Sum(x => x.TotalPrice) <= 0)
        {
            throw new ArgumentException("Price can't be zero or less.");
        }
        */

        return Task.FromResult(new TraktRequestStartResultDto
        {
            CheckoutLink = input.ReturnUrl + "?token=" + input.TraktRequestId
        });
    }

    public async Task<TraktRequestDto> CompleteAsync(ITraktRequestRepository traktRequestRepository, string token)
    {
        var traktRequest = await traktRequestRepository.GetAsync(Guid.Parse(token));

        traktRequest.SetAsCompleted();
        
        var myRequest = await traktRequestRepository.UpdateAsync(traktRequest);
        return MapToDto(myRequest);
    }

    private TraktRequestDto MapToDto(TraktRequest myRequest)
    {
        throw new NotImplementedException();
    }

    public Task HandleWebhookAsync(string payload)
    {
        return Task.CompletedTask;
    }
}