using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(FileServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class FileServiceApplicationContractsModule : AbpModule
{

}