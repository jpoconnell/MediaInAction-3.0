using MediaInAction.DelugeService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.DelugeService
{
    public abstract class DelugeServiceAppService : ApplicationService
    {
        protected DelugeServiceAppService()
        {
            LocalizationResource = typeof(DelugeServiceResource);
            ObjectMapperContext = typeof(DelugeServiceApplicationModule);
        }
    }
}
