using MediaInAction.PublicWeb.Components.Emby;
using MediaInAction.PublicWeb.Components.Toolbar.Cart;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.PublicWeb.Controllers;

[Route("Widgets")]
public class WidgetsController : AbpController
{
    [HttpGet]
    [Route("Emby")]
    public IActionResult Counters()
    {
        return ViewComponent(typeof(EmbyWidgetViewComponent));
    }

    [HttpGet]
    [Route("Cart")]
    public IActionResult GetCartWidget()
    {
        return ViewComponent(typeof(CartWidgetViewComponent));
    }
}