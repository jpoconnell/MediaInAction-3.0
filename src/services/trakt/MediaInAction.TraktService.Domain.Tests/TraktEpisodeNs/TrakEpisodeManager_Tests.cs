using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktShowNs;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeManager_Tests : TraktServiceDomainTestBase
    {
        private readonly TraktEpisodeManager _traktEpisodeManager;
        private readonly ITraktEpisodeRepository _traktEpisodeRepository;
        private readonly ITraktShowRepository _traktShowRepository;
        public TraktEpisodeManager_Tests()
        {
            _traktEpisodeManager = GetRequiredService<TraktEpisodeManager>();
            _traktEpisodeRepository = GetRequiredService<ITraktEpisodeRepository>();
            _traktShowRepository = GetRequiredService<ITraktShowRepository>();
        }

        [Fact]
        public async Task Should_Set_Email_Of_A_User()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var episode = await _traktEpisodeRepository.FirstOrDefaultAsync();
                episode.SeasonNum = 1;
                await _traktEpisodeRepository.UpdateAsync(episode);
            });

           var dbEpisodeList = await _traktEpisodeRepository.GetListAsync();
           var dbEpisode = dbEpisodeList[0];
           dbEpisode.SeasonNum.ShouldBe(1);
        }
        
        [Fact]
        public async Task Should_Create_A_New_Episode()
        {
            var existingShow = await _traktShowRepository.FirstOrDefaultAsync();

            var createEpisode = new TraktEpisodeCreateDto();
            createEpisode.ShowSlug = existingShow.Slug;
            createEpisode.SeasonNum = 1;
            createEpisode.EpisodeNum = 1;
            createEpisode.EpisodeName = "Episode 1";
            createEpisode.AiredDate = Convert.ToDateTime("2020-01-01");
            
            var createdSeries = await _traktEpisodeManager.CreateAsync(createEpisode);
        
            createdSeries.ShouldNotBeNull();
        }
    }
}
