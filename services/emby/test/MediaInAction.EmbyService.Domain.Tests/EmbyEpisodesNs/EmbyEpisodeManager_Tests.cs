using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyEpisodesNs
{
    public class EmbyEpisodeManager_Tests : EmbyServiceDomainTestBase
    {
        private readonly EmbyEpisodeManager _episodeManager;
        private readonly IEmbyEpisodeRepository _episodeRepository;
        
        public EmbyEpisodeManager_Tests()
        {
            _episodeManager = GetRequiredService<EmbyEpisodeManager>();
            _episodeRepository = GetRequiredService<IEmbyEpisodeRepository>();
        }

        [Fact]
        public async Task Should_Set_Email_Of_A_User()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var episode = await _episodeRepository.FirstOrDefaultAsync();
                episode.SeasonNum = 1;
                await _episodeRepository.UpdateAsync(episode);
            });

           var  dbEpisodeList = await _episodeRepository.GetListAsync();
           var dbEpisode = dbEpisodeList[0];
           dbEpisode.SeasonNum.ShouldBe(1);
        }
    }
}
