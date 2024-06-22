using Volo.Abp.Modularity;

namespace MediaInAction.PaymentService
{
    [DependsOn(
        typeof(PaymentServiceApplicationModule),
        typeof(PaymentServiceDomainTestModule)
        )]
    public class PaymentServiceApplicationTestModule : AbpModule
    {

    }
}
