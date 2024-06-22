using MediaInAction.EmbyService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.EmbyService
{
    public abstract class EmbyServiceAppService : ApplicationService
    {
        protected EmbyServiceAppService()
        {
            LocalizationResource = typeof(EmbyServiceResource);
            ObjectMapperContext = typeof(EmbyServiceApplicationModule);
        }
    }
}
