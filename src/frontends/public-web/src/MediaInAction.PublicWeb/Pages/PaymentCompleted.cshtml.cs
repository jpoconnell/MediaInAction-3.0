using MediaInAction.TraktService.TraktRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MediaInAction.PublicWeb.Pages;

[Authorize]
public class TraktCompletedModel : AbpPageModel
{
    private readonly ITraktRequestAppService _paymentRequestAppService;

    public TraktCompletedModel(ITraktRequestAppService paymentRequestAppService)
    {
        _paymentRequestAppService = paymentRequestAppService;
    }

    [BindProperty(SupportsGet = true)] public string Token { get; set; }

    public TraktRequestDto TraktRequest { get; private set; }

    public bool IsSuccessful { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!HttpContext.Request.Cookies.TryGetValue(MediaInActionTraktConsts.TraktMethodCookie,
                out var selectedTraktMethod))
        {
            throw new InvalidOperationException("A payment type must be selected!");
        }

        TraktRequest = await _paymentRequestAppService.CompleteAsync(
            // TODO: Use string name
            selectedTraktMethod,
            new TraktRequestCompleteInputDto() { Token = Token });

        IsSuccessful = TraktRequest.State == TraktRequestState.Completed;

        if (IsSuccessful)
        {
            // Remove cookie so that can be set again when default payment type is set
            HttpContext.Response.Cookies.Delete(MediaInActionTraktConsts.TraktMethodCookie);
            return RedirectToPage("OrderReceived", new { orderNo = TraktRequest.OrderNo });
        }

        return Page();
    }
}