using MediaInAction.TraktService.TraktMethods;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace MediaInAction.TraktService.Controllers;

[RemoteService(Name = TraktServiceRemoteServiceConsts.RemoteServiceName)]
[Area("payment")]
[Route("api/payment/methods")]
public class TraktMethodController : TraktServiceController, ITraktMethodAppService
{
    protected ITraktMethodAppService TraktMethodAppService { get; }

    public TraktMethodController(ITraktMethodAppService paymentMethodAppService)
    {
        TraktMethodAppService = paymentMethodAppService;
    }

    [HttpGet]
    public Task<List<TraktMethodDto>> GetListAsync()
    {
        return TraktMethodAppService.GetListAsync();
    }
}
