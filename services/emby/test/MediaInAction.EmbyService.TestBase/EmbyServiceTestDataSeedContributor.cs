using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasesNs;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyEpisodesNs;
using MediaInAction.EmbyService.EmbyMovieAliasesNs;
using MediaInAction.EmbyService.EmbyMoviesNs;
using MediaInAction.EmbyService.EmbyShowAliasesNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly EmbyShowManager _showManager;
        private readonly IEmbyShowRepository _embyShowRepository;
        private readonly EmbyMovieManager _movieManager;
        private readonly IEmbyMovieRepository _movieRepository;
        private readonly EmbyEpisodeManager _episodeManager;
        private readonly IEmbyEpisodeRepository _episodeRepository;
        private readonly TestData _testData;
        
        public EmbyServiceTestDataSeedContributor(
            EmbyShowManager showManager,
            IEmbyShowRepository embyShowRepository,
            EmbyMovieManager movieManager,
            IEmbyMovieRepository movieRepository,
            EmbyEpisodeManager episodeManager,
            IEmbyEpisodeRepository episodeRepository,
            TestData testData)
        {
            _showManager = showManager;
            _embyShowRepository = embyShowRepository;
            _movieManager = movieManager;
            _movieRepository = movieRepository;
            _episodeManager = episodeManager;
            _episodeRepository = episodeRepository;
            _testData = testData;
        }
        public Task SeedAsync(DataSeedContext context)
        {
             SeedTestEmbyServiceAsync();
             return Task.CompletedTask;
        }

        private async Task SeedTestEmbyServiceAsync()
        {
            var embyShowAliasCreateList = new List<EmbyShowAliasCreateDto>();
            
            var embyShowAliasCreate= new EmbyShowAliasCreateDto
            {
                IdType = "_testData.ShowType1",
                IdValue = "_testData.ShowYear1"
            };
            embyShowAliasCreateList.Add(embyShowAliasCreate);
            
            var embyShowCreate = new EmbyShowCreateDto
            {
                Name = _testData.ShowName1,
                FirstAiredYear = _testData.ShowYear1,
                EmbyShowAliasesCreateDto = embyShowAliasCreateList
            };
            var newSeries = await _showManager.CreateAsync(embyShowCreate);

            
            var embyMovieAliasCreateDtoList = new List<EmbyMovieAliasCreateDto>();
            var embyMovieAliasCreateDto = new EmbyMovieAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            embyMovieAliasCreateDtoList.Add(embyMovieAliasCreateDto);
            var embyMovieCreateDto = new EmbyMovieCreateDto
            {
                FirstAiredYear = _testData.MovieYear1,
                Name = _testData.MovieName1,
                EmbyMovieAliasCreateDtos = embyMovieAliasCreateDtoList
            };
            await _movieManager.CreateAsync(embyMovieCreateDto);
            
            
            var embyEpisodeAliasCreateDtoList = new List<EmbyEpisodeAliasCreateDto>();
            var embyEpisodeAliasCreateDto = new EmbyEpisodeAliasCreateDto
            {
                IdType = "_testData.MovieType1",
                IdValue = "_testData.MovieYear1"
            };
            embyEpisodeAliasCreateDtoList.Add(embyEpisodeAliasCreateDto);
            
           // var dbSeries = await _embyShowRepository.GetAsync(newSeries.Id);
            
            if (newSeries == null)
            {
                return;
            }
            else
            {
                var embyEpisodeCreateDto = new EmbyEpisodeCreateDto
                {
                    EmbySeriesId = newSeries.Id,
                    SeasonNum = _testData.SeasonNum1,
                    EpisodeNum = _testData.EpisodeNume1,
                    EmbyEpisodeAliasCreateDtos = embyEpisodeAliasCreateDtoList
                };
           
                await _episodeManager.CreateAsync(embyEpisodeCreateDto);
            }

        }
    }
}