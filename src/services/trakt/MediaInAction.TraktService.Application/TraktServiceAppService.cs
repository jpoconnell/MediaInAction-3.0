using MediaInAction.TraktService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService
{
    /* Inherit your application services from this class.
     */
    public abstract class TraktServiceAppService : ApplicationService
    {
        protected TraktServiceAppService()
        {
            LocalizationResource = typeof(TraktServiceResource);
        }
    }
}
