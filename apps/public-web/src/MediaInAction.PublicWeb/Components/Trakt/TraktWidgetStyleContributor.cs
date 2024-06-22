using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace MediaInAction.PublicWeb.Components.Trakt;

public class TraktWidgetStyleContributor: BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/components/payment/payment-widget.css");
    }
}