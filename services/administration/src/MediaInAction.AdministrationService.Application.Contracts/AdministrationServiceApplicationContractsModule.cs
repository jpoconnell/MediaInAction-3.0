using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

// using MediaInAction.CatalogService;
// using MediaInAction.OrderingService;
// using MediaInAction.CmskitService;

namespace MediaInAction.AdministrationService
{
    [DependsOn(
        typeof(AdministrationServiceDomainSharedModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule)
        // typeof(CatalogServiceApplicationContractsModule),
        // typeof(OrderingServiceApplicationContractsModule),
        // typeof(CmskitServiceApplicationContractsModule)
    )]
    public class AdministrationServiceApplicationContractsModule : AbpModule
    {
    }
}