using MediaInAction.EmbyService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.EmbyService
{
    public abstract class EmbyServiceController : AbpControllerBase
    {
        protected EmbyServiceController()
        {
            LocalizationResource = typeof(EmbyServiceResource);
        }
    }
}
