using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(FileServiceDomainSharedModule)
)]
public class FileServiceDomainModule : AbpModule
{

}