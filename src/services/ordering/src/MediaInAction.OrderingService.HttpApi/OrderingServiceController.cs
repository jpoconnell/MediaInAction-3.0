using MediaInAction.OrderingService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.OrderingService;

public abstract class OrderingServiceController : AbpControllerBase
{
    protected OrderingServiceController()
    {
        LocalizationResource = typeof(OrderingServiceResource);
    }
}