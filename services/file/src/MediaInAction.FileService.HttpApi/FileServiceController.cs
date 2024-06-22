using MediaInAction.FileService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.FileService;

public abstract class FileServiceController : AbpControllerBase
{
    protected FileServiceController()
    {
        LocalizationResource = typeof(FileServiceResource);
    }
}