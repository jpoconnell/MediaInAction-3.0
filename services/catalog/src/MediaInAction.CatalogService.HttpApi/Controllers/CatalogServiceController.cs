using MediaInAction.CatalogService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MediaInAction.CatalogService.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CatalogServiceController : AbpControllerBase
    {
        protected CatalogServiceController()
        {
            LocalizationResource = typeof(CatalogServiceResource);
        }
    }
}