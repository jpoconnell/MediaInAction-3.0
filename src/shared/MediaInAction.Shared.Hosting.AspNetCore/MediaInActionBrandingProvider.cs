using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MediaInAction.Shared.Hosting.AspNetCore
{
    [Dependency(ReplaceServices = true)]
    public class MediaInActionBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MediaInAction";
    }
}
