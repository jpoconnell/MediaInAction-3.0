
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(

        )]
    public class EmbyServiceDomainTestModule : AbpModule
    {
        
    }
}
