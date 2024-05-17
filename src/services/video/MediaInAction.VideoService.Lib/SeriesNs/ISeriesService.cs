using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesService
{
    Task<SeriesDto> GetByIdAsync(Guid link);
    Task<List<SeriesDto>> GetActiveList();
    Task<List<SeriesDto>> GetAllNoDefault();
    Task<string> GetSlugAsync(Guid seriesId);
}