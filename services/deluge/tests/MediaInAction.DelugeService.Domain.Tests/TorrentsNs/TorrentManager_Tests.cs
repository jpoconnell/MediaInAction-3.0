using System.Threading.Tasks;
using MediaInAction.DelugeService.TorrentNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.DelugeService.TorrentsNs
{
    public class TorrentManager_Tests : DelugeServiceDomainTestBase
    {
        private readonly TorrentManager _torrentManager;
        private readonly ITorrentRepository _torrentRepository;
        
        public TorrentManager_Tests()
        {
            _torrentManager = GetRequiredService<TorrentManager>();
            _torrentRepository = GetRequiredService<ITorrentRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {

            await WithUnitOfWorkAsync(async () =>
            {
                var torrent = await _torrentRepository.FirstOrDefaultAsync();
                torrent.Name = "FBI";
                await _torrentRepository.UpdateAsync(torrent);
            });

            var  dbShowList = await _torrentRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }
        
        
        [Fact]
        public async Task Should_Get_List()
        {
            var dbShowList = await _torrentRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("ssss");
        }
    }
}
