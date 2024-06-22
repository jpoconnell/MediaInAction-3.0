using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public interface IEmbyEpisodeRepository :  IRepository<EmbyEpisode, Guid>
{
    Task<EmbyEpisode> GetBySeriesSeasonEpisodeAsync(string seriesId, int season,int episode);
    Task<List<EmbyEpisode>> GetListPagedAsync(ISpecification<EmbyEpisode> specification, int inputSkipCount, int inputMaxResultCount, string inputSorting);
    Task<EmbyEpisode> GetByEmbyShowSeasonEpisodeAsync(string showName, 
        int season, int episode);
}