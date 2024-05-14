using MediaInAction.CmskitService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.CmskitService;

public abstract class CmskitServiceController : AbpControllerBase
{
    protected CmskitServiceController()
    {
        LocalizationResource = typeof(CmskitServiceResource);
    }
}
