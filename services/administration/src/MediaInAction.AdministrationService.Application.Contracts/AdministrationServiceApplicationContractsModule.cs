using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

// using MediaInAction.VideoService;
// using MediaInAction.FileService;
// using MediaInAction.CmskitService;

namespace MediaInAction.AdministrationService
{
    [DependsOn(
        typeof(AdministrationServiceDomainSharedModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule)
        // typeof(VideoServiceApplicationContractsModule),
        // typeof(FileServiceApplicationContractsModule),
        // typeof(CmskitServiceApplicationContractsModule)
    )]
    public class AdministrationServiceApplicationContractsModule : AbpModule
    {
    }
}