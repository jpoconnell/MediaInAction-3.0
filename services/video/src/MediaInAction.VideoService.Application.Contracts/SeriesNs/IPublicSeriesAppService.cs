using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs
{
    public interface IPublicSeriesAppService : IApplicationService
    {
        Task<List<SeriesDto>> GetListAsync();
        Task<SeriesDto> GetAsync(Guid id);
        Task<List<SeriesDto>> GetSeriesListAsync(GetSeriesListInput filter);
    }
}