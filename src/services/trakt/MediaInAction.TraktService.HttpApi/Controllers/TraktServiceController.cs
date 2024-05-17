using MediaInAction.TraktService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.TraktService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TraktServiceController : AbpControllerBase
    {
        protected TraktServiceController()
        {
            LocalizationResource = typeof(TraktServiceResource);
        }
    }
}