using MediaInAction.CmskitService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.CmskitService;

public abstract class CmskitServiceAppService : ApplicationService
{
    protected CmskitServiceAppService()
    {
        LocalizationResource = typeof(CmskitServiceResource);
        ObjectMapperContext = typeof(CmskitServiceApplicationModule);
    }
}
