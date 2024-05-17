using MediaInAction.DelugeService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.DelugeService
{
    public abstract class DelugeServiceController : AbpControllerBase
    {
        protected DelugeServiceController()
        {
            LocalizationResource = typeof(DelugeServiceResource);
        }
    }
}
