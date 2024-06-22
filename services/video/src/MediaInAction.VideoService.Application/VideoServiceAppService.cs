using MediaInAction.VideoService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService
{
    /* Inherit your application services from this class.
     */
    public abstract class VideoServiceAppService : ApplicationService
    {
        protected VideoServiceAppService()
        {
            LocalizationResource = typeof(VideoServiceResource);
        }
    }
}
