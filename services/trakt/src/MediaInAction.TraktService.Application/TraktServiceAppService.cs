using MediaInAction.TraktService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService
{
    public abstract class TraktServiceAppService : ApplicationService
    {
        protected TraktServiceAppService()
        {
            LocalizationResource = typeof(TraktServiceResource);
            ObjectMapperContext = typeof(TraktServiceApplicationModule);
        }
    }
}
