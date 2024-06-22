using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediaInAction.PublicWeb.ServiceProviders;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace MediaInAction.PublicWeb.Components.Toolbar.Cart;

[Widget(
    AutoInitialize = true,
    RefreshUrl = "/Widgets/Cart",
    StyleTypes = new[] {typeof(CartWidgetStyleContributor)},
    ScriptTypes = new[] {typeof(CartWidgetScriptContributor)}
)]
public class CartWidgetViewComponent : AbpViewComponent
{
    private readonly UserEmbyProvider _userEmbyProvider;

    public CartWidgetViewComponent(UserEmbyProvider userEmbyProvider)
    {
        _userEmbyProvider = userEmbyProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(
            "~/Components/Toolbar/Cart/Default.cshtml",
            await _userEmbyProvider.GetEmbyAsync());
    }
}