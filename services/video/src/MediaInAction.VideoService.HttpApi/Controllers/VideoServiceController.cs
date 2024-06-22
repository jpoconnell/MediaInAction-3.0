using MediaInAction.VideoService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.VideoService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class VideoServiceController : AbpControllerBase
    {
        protected VideoServiceController()
        {
            LocalizationResource = typeof(VideoServiceResource);
        }
    }
}