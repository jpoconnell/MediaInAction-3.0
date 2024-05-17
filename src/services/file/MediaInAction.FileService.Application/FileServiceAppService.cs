using MediaInAction.FileService.Localization;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService
{
    /* Inherit your application services from this class.
     */
    public abstract class FileServiceAppService : ApplicationService
    {
        protected FileServiceAppService()
        {
            LocalizationResource = typeof(FileServiceResource);
        }
    }
}
