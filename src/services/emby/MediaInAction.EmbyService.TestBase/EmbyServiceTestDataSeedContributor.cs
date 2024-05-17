using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyShowsNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly EmbyShowManager _showManager;
        private readonly IEmbyShowRepository _showRepository;
        private readonly EmbyMovieManager _movieManager;
        private readonly IEmbyMovieRepository _movieRepository;
        private readonly EmbyEpisodeManager _episodeManager;
        private readonly IEmbyEpisodeRepository _episodeRepository;
        private readonly TestData _testData;
        
        public EmbyServiceTestDataSeedContributor(
            EmbyShowManager showManager,
            IEmbyShowRepository showRepository,
            EmbyMovieManager movieManager,
            IEmbyMovieRepository movieRepository,
            EmbyEpisodeManager episodeManager,
            IEmbyEpisodeRepository episodeRepository,
            TestData testData)
        {
            _showManager = showManager;
            _showRepository = showRepository;
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
            var showAliases = new List<( string idType, string idValue)>();
            
            showAliases.Add(("type", "value"));
            var show = await _showManager.CreateAsync(
                _testData.ShowName1,
                _testData.ShowYear1,
                showAliases
            );

            var showAliases2 = new List<( string idType, string idValue)>();
            
            showAliases2.Add(("type2", "value2"));
            await _showManager.CreateAsync(
                _testData.ShowName2,
                _testData.ShowYear2,
                showAliases2
            );
            
            var movieAliases1 = new List<( string idType, string idValue)>();
            movieAliases1.Add(("type", "value"));
            await _movieManager.CreateAsync(
              
                _testData.MovieName1,
                _testData.MovieYear1,
                movieAliases1
            );
            
            var movieAliases2 = new List<( string idType, string idValue)>();
            movieAliases2.Add(("type", "value"));
            await _movieManager.CreateAsync(
                _testData.MovieName2,
                _testData.MovieYear2,
                movieAliases2
            );
            
            var showSlug = "fbi";
            var episodeAliases1 = new List<( string idType, string idValue)>();
            episodeAliases1.Add(("type", "value"));
            await _episodeManager.CreateAsync(
                showSlug,
                    1,2
            );
            
            var episodeAliases2 = new List<( string idType, string idValue)>();
            episodeAliases2.Add(("type", "value"));
            await _episodeManager.CreateAsync(
                showSlug,
                1,3
            );
        }
    }
}