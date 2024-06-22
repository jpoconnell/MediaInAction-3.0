using JetBrains.Annotations;
using System.Collections.Generic;

namespace MediaInAction.PublicWeb.TraktMethods;

public class TraktMethodUiOptions
{
    [NotNull]
    public Dictionary<string, string> Icons { get; } = new();

    public string DefaultIcon { get; set; } = "fa-credit-card demo";

    public void ConfigureIcon(string paymentMethod, string iconCss)
    {
        Icons[paymentMethod] = iconCss;
    }
}
