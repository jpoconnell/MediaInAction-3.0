using MediaInAction.TraktService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.TraktService
{
    public abstract class TraktServiceController : AbpControllerBase
    {
        protected TraktServiceController()
        {
            LocalizationResource = typeof(TraktServiceResource);
        }
    }
}
