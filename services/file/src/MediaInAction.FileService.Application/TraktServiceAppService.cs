using MediaInAction.FileService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService
{
    public abstract class FileServiceAppService : ApplicationService
    {
        protected FileServiceAppService()
        {
            LocalizationResource = typeof(FileServiceResource);
            ObjectMapperContext = typeof(FileServiceApplicationModule);
        }
    }
}
