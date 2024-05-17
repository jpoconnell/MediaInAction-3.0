using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs;

    public interface ISeriesAppService : IApplicationService
    {
        Task<SeriesDto> CreateAsync(SeriesCreateDto input);
        Task<SeriesDto> GetAsync(Guid id);
        Task<SeriesDto> GetSeriesAsync(GetSeriesInput input);
        Task SetAsInActiveAsync(Guid id);
        Task<PagedResultDto<SeriesDto>> GetSeriesListPagedAsync(GetSeriesListInput input);
        Task<DashboardDto> GetSeriesDashboardAsync(DashboardInput input);
        Task ExportSeriesDataAsync();
        Task<List<SeriesDto>> GetSeriesListAsync(GetSeriesListInput input);
    }
