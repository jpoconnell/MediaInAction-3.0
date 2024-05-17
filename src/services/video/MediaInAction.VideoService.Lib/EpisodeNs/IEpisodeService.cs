using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.ParserNs;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs;

public interface IEpisodeService
{
    Task<EpisodeDto> FindBySeriesIdSeasonEpisodeNum(Guid id, int seasonNum, int episodeNum);
    Task<EpisodeDto> GetByIdAsync(Guid fileEntryLink);
    Task UpdateAsync(EpisodeDto episodeDto);
    Task CreateAsync(ParserDto parser);
    Task<List<EpisodeDto>> EpisodesNoDate(Guid seriesId);
    
    Task<List<EpisodeDto>> GetBySpec(ISpecification<Episode> specification);
    Task<List<EpisodeDto>> EpisodeFilesToMove();

}