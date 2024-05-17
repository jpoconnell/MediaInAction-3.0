using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MediaInAction.PublicWeb;

[Dependency(ReplaceServices = true)]
public class MediaInActionBrandingProvider: DefaultBrandingProvider
{
    public override string AppName => "MediaInAction";
}