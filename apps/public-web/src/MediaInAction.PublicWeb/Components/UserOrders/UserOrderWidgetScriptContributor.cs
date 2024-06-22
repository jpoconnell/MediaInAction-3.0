﻿using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace MediaInAction.PublicWeb.Components.UserOrders;

public class UserOrderWidgetScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/components/user-orders/user-order-widget.js");
    }
}