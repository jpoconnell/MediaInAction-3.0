using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowManager_Tests : TraktServiceDomainTestBase
    {
        private readonly TraktShowManager _traktShowManager;

        private readonly ITraktShowRepository _traktShowRepository;
        
        public TraktShowManager_Tests()
        {
            _traktShowManager = GetRequiredService<TraktShowManager>();
            _traktShowRepository = GetRequiredService<ITraktShowRepository>();
        }
        
        [Fact]
        public async Task ShouldError_DuplicateName()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                var createShow = new TraktShowCreateDto();
                createShow.Name = "FBI";
                createShow.FirstAiredYear = 2018;
                createShow.Slug = "fbi";
                TraktShow show = await _traktShowManager.CreateAsync(createShow);
                show.ShouldBeNull();
            });
        }
    }
}
