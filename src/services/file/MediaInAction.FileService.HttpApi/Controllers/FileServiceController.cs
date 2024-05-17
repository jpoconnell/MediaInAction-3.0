using MediaInAction.FileService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.FileService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class FileServiceController : AbpControllerBase
    {
        protected FileServiceController()
        {
            LocalizationResource = typeof(FileServiceResource);
        }
    }
}