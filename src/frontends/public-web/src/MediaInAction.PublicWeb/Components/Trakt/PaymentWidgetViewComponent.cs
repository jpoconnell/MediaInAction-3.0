using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.BasketService.Services;
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
    private readonly UserBasketProvider _userBasketProvider;
    private readonly UserAddressProvider _userAddressProvider;
    private readonly TraktMethodProvider _paymentMethodProvider;

    public TraktWidgetViewComponent(
        UserBasketProvider userBasketProvider,
        UserAddressProvider userAddressProvider,
        TraktMethodProvider paymentMethodProvider)
    {
        _userBasketProvider = userBasketProvider;
        _userAddressProvider = userAddressProvider;
        _paymentMethodProvider = paymentMethodProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var viewModel = new TraktViewModel
        {
            Basket = await _userBasketProvider.GetBasketAsync(),
            Address = _userAddressProvider.GetDemoAddresses(),
            TraktMethods = await _paymentMethodProvider.GetTraktMethodsAsync()
        };
        return View("~/Components/Trakt/Default.cshtml", viewModel);
    }
}

public class TraktViewModel
{
    public BasketDto Basket { get; set; }
    public List<AddressDto> Address { get; set; }
    public List<TraktMethodViewModel> TraktMethods { get; set; }
}