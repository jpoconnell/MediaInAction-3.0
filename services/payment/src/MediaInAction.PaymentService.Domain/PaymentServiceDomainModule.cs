using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.PaymentService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(PaymentServiceDomainSharedModule)
    )]
    public class PaymentServiceDomainModule : AbpModule
    {

    }
}
