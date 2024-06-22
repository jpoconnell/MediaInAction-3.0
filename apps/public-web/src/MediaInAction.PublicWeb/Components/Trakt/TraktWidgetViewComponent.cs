using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Services;
using MediaInAction.PublicWeb.ServiceProviders;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace MediaInAction.PublicWeb.Components.Trakt;

[Widget(
    AutoInitialize = true,
    RefreshUrl = "/Widgets/Trakt",
    StyleTypes = new[] { typeof(TraktWidgetStyleContributor) },
    ScriptTypes = new[] { typeof(TraktWidgetScriptContributor) }
)]
public class TraktWidgetViewComponent : AbpViewComponent
{
    private readonly UserEmbyProvider _userEmbyProvider;
    private readonly UserAddressProvider _userAddressProvider;
    private readonly TraktMethodProvider _paymentMethodProvider;

    public TraktWidgetViewComponent(
        UserEmbyProvider userEmbyProvider,
        UserAddressProvider userAddressProvider,
        TraktMethodProvider paymentMethodProvider)
    {
        _userEmbyProvider = userEmbyProvider;
        _userAddressProvider = userAddressProvider;
        _paymentMethodProvider = paymentMethodProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new TraktViewModel
        {
            Emby = await _userEmbyProvider.GetEmbyAsync(),
            Address = _userAddressProvider.GetDemoAddresses(),
            TraktMethods = await _paymentMethodProvider.GetTraktMethodsAsync()
        };
        return View("~/Components/Trakt/Default.cshtml", viewModel);
    }
}

public class TraktViewModel
{
    public EmbyDto Emby { get; set; }
    public List<AddressDto> Address { get; set; }
    public List<TraktMethodViewModel> TraktMethods { get; set; }
}