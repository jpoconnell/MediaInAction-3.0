using MediaInAction.PaymentService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.PaymentService
{
    public abstract class PaymentServiceController : AbpControllerBase
    {
        protected PaymentServiceController()
        {
            LocalizationResource = typeof(PaymentServiceResource);
        }
    }
}
