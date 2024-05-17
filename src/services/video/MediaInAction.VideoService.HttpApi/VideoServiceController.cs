using MediaInAction.VideoService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.VideoService;

public abstract class VideoServiceController : AbpControllerBase
{
    protected VideoServiceController()
    {
        LocalizationResource = typeof(VideoServiceResource);
    }
}