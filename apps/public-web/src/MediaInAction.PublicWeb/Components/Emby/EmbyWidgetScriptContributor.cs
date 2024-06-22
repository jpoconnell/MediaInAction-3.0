using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.SignalR;
using Volo.Abp.Modularity;

namespace MediaInAction.PublicWeb.Components.Emby;

[DependsOn(typeof(SignalRBrowserScriptContributor))]
public class EmbyWidgetScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/components/emby/emby-widget.js");
    }
}