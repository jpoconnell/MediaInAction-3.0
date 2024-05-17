using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.EpisodeNs.Specifications;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;
using Volo.Abp.Users;

namespace MediaInAction.VideoService.EpisodeNs
{
    public class EpisodeAppService : VideoServiceAppService, IEpisodeAppService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly EpisodeManager _episodeManager;
        private readonly ILogger<EpisodeAppService> _logger;
        
        public EpisodeAppService(IEpisodeRepository episodeRepository,
            EpisodeManager episodeManager,
                ILogger<EpisodeAppService> logger)
        {
            _episodeRepository = episodeRepository;
            _episodeManager = episodeManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<ListResultDto<EpisodeDto>> GetListAsync()
        {
            return new ListResultDto<EpisodeDto>(
                ObjectMapper.Map<List<Episode>, List<EpisodeDto>>(
                    await _episodeRepository.GetListAsync()
                )
            );
        }

        public async Task<EpisodeDto> CreateAsync(EpisodeCreateDto input)
        {
            //var episodeAliases = GetEpisodeAliasTuple(input.EpisodeAliases);
            var newEpisode = await _episodeManager.CreateAsync(input);
            return CreateEpisodeDtoMapping(newEpisode);
        }

        Task<EpisodeDto> IEpisodeAppService.GetAsync(Guid id)
        {
            return GetAsync(id);
        }

        Task<EpisodeDto> IEpisodeAppService.GetEpisodeAsync(GetEpisodeInput input)
        {
            return GetEpisodeAsync(input);
        }

        Task<List<EpisodeDto>> IEpisodeAppService.GetEpisodesAsync(GetEpisodesInput input)
        {
            return GetEpisodesAsync(input);
        }
        

        public async Task<EpisodeDto> GetAsync(Guid id)
        {
            var episode = await _episodeRepository.GetAsync(id);
            return ObjectMapper.Map<Episode, EpisodeDto>(episode);
        }

        public async Task<EpisodeDto> GetEpisodeAsync(GetEpisodeInput input)
        {
            var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
                input.SeriesId,input.SeasonNum, input.EpisodeNum);
            return CreateEpisodeDtoMapping(episode);
        } 

        public async Task<List<EpisodeDto>> GetEpisodesBySpecAsync(GetEpisodesInput input)
        {
            if (CurrentUser.Id == null)
            {
                return new List<EpisodeDto>();
            }
    
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetEpisodesByUserId(CurrentUser.GetId(), specification, true);
            return CreateEpisodeDtoMapping(episodes);
        }

        public async Task<List<EpisodeDto>> GetEpisodesAsync(GetEpisodesInput input)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetListAsync(specification, true);
            return CreateEpisodeDtoMapping(episodes);
        }

        [Authorize(VideoServicePermissions.Episodes.Update)]
        public async Task UpdateAsync(Guid id, EpisodeDto input)
        {
            var episodeList = await _episodeRepository.GetAsync(id);
          
            //  mediaLocationAlias.SetFileName(input.FullPath);
        }
        
        [Authorize(VideoServicePermissions.Episodes.SetAsComplete)]
        public async Task SetAsCompleteAsync(Guid id)
        {
            await _episodeManager.SetStatusAsync(id, MediaStatus.Complete);
        }

        [Authorize(VideoServicePermissions.Episodes.SetAsWatched)]
        public async Task SetAsWatchedAsync(Guid id)
        {
            await _episodeManager.SetStatusAsync(id,MediaStatus.Watched);
        }

        
        Task<DashboardDto> IEpisodeAppService.GetDashboardAsync(DashboardInput input)
        {
            return GetDashboardAsync(input);
        }

        public async Task<EpisodeDto> AcceptTraktEpisodeAsync(EpisodeCreateDto input)
        {
            var newEpisode = await CreateAsync(input);
            return newEpisode;
        }
        
        public async Task<List<EpisodeDto>> GetMyEpisodesAsync(GetMyEpisodesInput input)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetEpisodesByUserId(CurrentUser.GetId(), specification, true);
            return CreateEpisodeDtoMapping(episodes);
        }

        public async Task<PagedResultDto<EpisodeDto>> GetListPagedAsync(GetEpisodesInput input)
        { 
            ISpecification<Episode> specification = Specifications.SpecificationFactory.Create("a:");

            var episodeList =
                await _episodeRepository.GetListPagedAsync(specification, input.SkipCount,
                    input.MaxResultCount, "EpisodeName",true );

            var episodeDtoList = CreateEpisodeDtoMapping(episodeList);
            var totalCount = await _episodeRepository.GetCountAsync();
            return new PagedResultDto<EpisodeDto>(totalCount,episodeDtoList);
        }
        public async Task<DashboardDto> GetDashboardAsync(DashboardInput input)
        {
            return new DashboardDto()
            {
                EpisodeStatusDto = await GetCountOfTotalEpisodeStatusAsync(input.Filter),
            };
        }

        private async Task<List<EpisodeDto>> GetCountOfTotalEpisodeStatusAsync(string inputFilter)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(inputFilter);
            var episodes = await _episodeRepository.GetDashboardAsync(specification);
            return CreateEpisodeDtoMapping(episodes);
        }

        private List<( string idType, string idValue
            )> GetEpisodeAliasTuple(List<EpisodeAliasCreateDto> inEpisodeAliases)
        {
            var seriesAliases =
                new List<(  string idType, string idValue)>();
            foreach (var seriesAlias in inEpisodeAliases)
            {
                seriesAliases.Add((  seriesAlias.IdType, seriesAlias.IdValue ));
            }
            return seriesAliases;
        }
        
        private List<EpisodeDto> CreateEpisodeDtoMapping(List<Episode> episodes)
        {
            List<EpisodeDto> dtoList = new List<EpisodeDto>();
            foreach (var episode in episodes)
            {
                dtoList.Add(CreateEpisodeDtoMapping(episode));
            }

            return dtoList;
        }

        private EpisodeDto CreateEpisodeDtoMapping(Episode episode)
        {
            if (episode != null)
            {
                return new EpisodeDto()
                {
                    Id = episode.Id,
                    SeriesId = episode.SeriesId,
                    SeasonNum = episode.SeasonNum,
                    EpisodeNum = episode.EpisodeNum,
                };
            }
            else
            {
                return null;
            }

        }
    }
}
