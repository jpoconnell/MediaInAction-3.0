using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediaInAction.FileService.Products;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MediaInAction.PublicWeb.Pages
{
    public class IndexModel : AbpPageModel
    {
        public IReadOnlyList<ProductDto> Products { get; private set; }
        public bool HasRemoteServiceError { get; set; } = false; 
        private readonly IPublicProductAppService _productAppService;

        public IndexModel(IPublicProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public async Task OnGet()
        {
            try
            {
                Products = (await _productAppService.GetListAsync()).Items;
            }
            catch (Exception e)
            {
                Products = new ReadOnlyCollection<ProductDto>(new List<ProductDto>());
                HasRemoteServiceError = true;
                Console.WriteLine(e);
            }
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}