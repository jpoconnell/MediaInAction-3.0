using System;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktRequests;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.TraktMethods;

[ExposeServices(typeof(ITraktMethod), typeof(CalendarTraktMethod))]
public class CalendarTraktMethod : ITraktMethod
{
    public string Name => TraktMethodNames.Calendar;

    public Task<TraktRequestStartResultDto> StartAsync(TraktRequest traktRequest, TraktRequestStartDto input)
    {
        if (traktRequest.Products.Sum(x => x.TotalPrice) <= 0)
        {
            throw new ArgumentException("Price can't be zero or less.");
        }

        return Task.FromResult(new TraktRequestStartResultDto
        {
            CheckoutLink = input.ReturnUrl + "?token=" + input.TraktRequestId
        });
    }

    public async Task<TraktRequest> CompleteAsync(ITraktRequestRepository traktRequestRepository, string token)
    {
        var traktRequest = await traktRequestRepository.GetAsync(Guid.Parse(token));

        traktRequest.SetAsCompleted();

        return await traktRequestRepository.UpdateAsync(traktRequest);
    }

    public Task HandleWebhookAsync(string payload)
    {
        return Task.CompletedTask;
    }
}