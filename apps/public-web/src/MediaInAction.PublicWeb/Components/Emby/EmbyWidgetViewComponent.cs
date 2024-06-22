using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediaInAction.PublicWeb.ServiceProviders;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace MediaInAction.PublicWeb.Components.Emby;

[Widget(
    AutoInitialize = true,
    RefreshUrl = "/Widgets/Emby",
    StyleFiles = new[] {"/components/emby/emby-widget.css"},
    ScriptTypes = new[] {typeof(EmbyWidgetScriptContributor)}
)]
public class EmbyWidgetViewComponent : AbpViewComponent
{
    private readonly UserEmbyProvider _userEmbyProvider;

    public EmbyWidgetViewComponent(UserEmbyProvider userEmbyProvider)
    {
        _userEmbyProvider = userEmbyProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(
            "~/Components/Emby/Default.cshtml",
            await _userEmbyProvider.GetEmbyAsync());
    }
}