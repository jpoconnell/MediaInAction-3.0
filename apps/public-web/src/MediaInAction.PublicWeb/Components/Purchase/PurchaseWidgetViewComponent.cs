using System.Threading.Tasks;
using MediaInAction.EmbyService.Services;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;

namespace MediaInAction.PublicWeb.Components.Purchase;

[Widget(
    AutoInitialize = true,
    RefreshUrl = "/Widgets/Emby",
    StyleTypes = new[] {typeof(PurchaseWidgetStyleContributor)},
    ScriptTypes = new[] {typeof(PurchaseWidgetScriptContributor)}
)]
public class PurchaseWidgetViewComponent : AbpViewComponent
{
    public PurchaseViewModel ViewModel { get; set; }

    public Task<IViewComponentResult> InvokeAsync(string buttonDescription, EmbyDto emby)
    {
        ViewModel = new PurchaseViewModel
        {
            ButtonDescription = buttonDescription, 
            Emby = emby
        };

        return Task.FromResult<IViewComponentResult>(
            View("~/Components/Purchase/Default.cshtml", ViewModel)
        );
    }

    public class PurchaseViewModel
    {
        public string ButtonDescription { get; set; }
        public EmbyDto Emby { get; set; }
    }
}