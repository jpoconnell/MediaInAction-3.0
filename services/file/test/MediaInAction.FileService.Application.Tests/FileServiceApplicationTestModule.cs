using Volo.Abp.Modularity;

namespace MediaInAction.OrderingService
{
    [DependsOn(
        typeof(OrderingServiceApplicationModule),
        typeof(OrderingServiceDomainTestModule)
        )]
    public class OrderingServiceApplicationTestModule : AbpModule
    {

    }
}
