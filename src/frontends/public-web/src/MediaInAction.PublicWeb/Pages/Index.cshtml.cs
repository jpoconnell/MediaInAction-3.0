using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MediaInAction.PublicWeb.Pages
{
    public class IndexModel : AbpPageModel
    {
        public IReadOnlyList<SeriesDto> SeriesDtos { get; private set; }
        public bool HasRemoteServiceError { get; set; } = false; 
        private readonly IPublicSeriesAppService _seriesAppService;

        public IndexModel(IPublicSeriesAppService seriesAppService)
        {
            _seriesAppService = seriesAppService;
        }

        public async Task OnGet()
        {
            try
            {
                SeriesDtos = (await _seriesAppService.GetListAsync());
            }
            catch (Exception e)
            {
                SeriesDtos = new ReadOnlyCollection<SeriesDto>(new List<SeriesDto>());
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