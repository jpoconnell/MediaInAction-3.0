using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.DelugeService
{
    public class DelugeServiceDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly TorrentManager _torrentManager;
        private readonly ITorrentRepository _torrentRepository;
        private readonly TestData _testData;
        
        public DelugeServiceDataSeedContributor(
            TorrentManager torrentManager,
            ITorrentRepository torrentRepository,
            TestData testData)
        {
            _torrentManager = torrentManager;
            _torrentRepository = torrentRepository;
            _testData = testData;
        }

        public Task SeedAsync(DataSeedContext context)
        {
             SeedTestDelugeServiceAsync();
            return Task.CompletedTask;
        }

        public async Task SeedTestDelugeServiceAsync()
        {
            await _torrentManager.CreateAsync(
                "no comment",
                "ssss",
                false,
                "hash1",
                false,
                0.0,
                "no message",
                "label",
                2000,
                0.0,
                "/downloads"
            );
                        
            await _torrentManager.CreateAsync(
                "no comment",
                _testData.TorrentName2,
                false,
                "hash2",
                false,
                0,
                "no message",
                "label",
                2000,0.0,
                "/downloads"
            );
            
        }
    }
}
