using MediaInAction.TraktService.TraktRequests;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace MediaInAction.TraktService.Controllers;

[RemoteService(Name = TraktServiceRemoteServiceConsts.RemoteServiceName)]
[Area("payment")]
[Route("api/payment/requests")]
public class TraktRequestController : TraktServiceController, ITraktRequestAppService
{
    protected ITraktRequestAppService TraktRequestAppService { get; }

    public TraktRequestController(ITraktRequestAppService paymentRequestAppService)
    {
        TraktRequestAppService = paymentRequestAppService;
    }

    [HttpPost("{paymentMethod}/complete")]
    public Task<TraktRequestDto> CompleteAsync(string paymentMethod, TraktRequestCompleteInputDto input)
    {
        return TraktRequestAppService.CompleteAsync(paymentMethod, input);
    }

    [HttpPost]
    public Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input)
    {
        return TraktRequestAppService.CreateAsync(input);
    }

    [HttpPost]
    [Route("{paymentMethod}/webhook")]
    public async Task<bool> HandleWebhookAsync(string paymentMethod, string payload)
    {
        var bytes = await Request.Body.GetAllBytesAsync();
        payload = Encoding.UTF8.GetString(bytes);

        return await TraktRequestAppService.HandleWebhookAsync(paymentMethod, payload);
    }

    [HttpPost("{paymentMethod}/start")]
    public Task<TraktRequestStartResultDto> StartAsync(string paymentMethod, TraktRequestStartDto input)
    {
        return TraktRequestAppService.StartAsync(paymentMethod, input);
    }
}
