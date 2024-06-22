using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyMoviesNs
{
    public class EmbyMovieManager_Tests : EmbyServiceDomainTestBase
    {
        private readonly EmbyMovieManager _traktMovieManager;
        private readonly IEmbyMovieRepository _traktMovieRepository;
        
        public EmbyMovieManager_Tests()
        {
            _traktMovieManager = GetRequiredService<EmbyMovieManager>();
            _traktMovieRepository = GetRequiredService<IEmbyMovieRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var show = await _traktMovieRepository.FirstOrDefaultAsync();
                show.Name = "FBI";
                await _traktMovieRepository.UpdateAsync(show);
            });

            var  dbShowList = await _traktMovieRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }

    }
}
