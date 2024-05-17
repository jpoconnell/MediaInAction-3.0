using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieManager_Tests : TraktServiceDomainTestBase
    {
        private readonly TraktMovieManager _traktMovieManager;
        private readonly ITraktMovieRepository _traktMovieRepository;
        
        public TraktMovieManager_Tests()
        {
            _traktMovieManager = GetRequiredService<TraktMovieManager>();
            _traktMovieRepository = GetRequiredService<ITraktMovieRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var movie = await _traktMovieRepository.FirstOrDefaultAsync();
                movie.Name = "FBI";
                await _traktMovieRepository.UpdateAsync(movie);
            });

            var  dbMovieList = await _traktMovieRepository.GetListAsync();
            var dbShow = dbMovieList[0];
            dbShow.Name.ShouldBe("FBI");
        }

    }
}
