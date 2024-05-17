using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService
{
    public class TraktServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly LoadShowList _loadShowList;
        private readonly LoadMovieList _loadMovieList;
        private readonly LoadEpisodeList _loadEpisodeList;
        private readonly TestData _testData;
        
        public TraktServiceTestDataSeedContributor(
            LoadShowList loadShowList,
            LoadMovieList loadMovieList,
            LoadEpisodeList loadEpisodeList,
            TestData testData)
        {
            _loadShowList = loadShowList;
            _loadMovieList = loadMovieList;
            _loadEpisodeList = loadEpisodeList;
            _testData = testData;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedTestTraktServiceAsync();
        }

        private async Task SeedTestTraktServiceAsync()
        {
            await SeedShowAsync();
            await SeedEpisodeAsync();
            await SeedMovieAsync();
        }

        private async Task SeedEpisodeAsync()
        {
            var rowCnt = await _loadEpisodeList.GetCount();

            if (rowCnt > 0)
            {
                return;
            }
            await _loadEpisodeList.LoadEpisodeData();
            
        }
        
        private async Task SeedMovieAsync()
        {
            var rowCnt = await _loadMovieList.GetCount();

            if (rowCnt > 0)
            {
                return;
            }
            await _loadMovieList.LoadMovieData();
            
        }
        
        private async Task SeedShowAsync()
        {
            var rowCnt = await _loadShowList.GetCount();

            if (rowCnt > 0)
            {
                return;
            }
            await _loadShowList.LoadShowData();
            
        }
    }
    
}