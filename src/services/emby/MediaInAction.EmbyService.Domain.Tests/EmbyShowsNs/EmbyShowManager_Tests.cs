using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.EmbyService.EmbyShowsNs
{
    public class EmbyShowManager_Tests : EmbyServiceDomainTestBase
    {
        private readonly EmbyShowManager _embyShowManager;

        private readonly IEmbyShowRepository _embyShowRepository;
        
        public EmbyShowManager_Tests()
        {
            _embyShowManager = GetRequiredService<EmbyShowManager>();
            _embyShowRepository = GetRequiredService<IEmbyShowRepository>();
        }
        
        [Fact]
        public async Task Should_Set_Name()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                var show = await _embyShowRepository.FirstOrDefaultAsync();
                show.Name = "FBI";
                await _embyShowRepository.UpdateAsync(show);
            });

            var  dbShowList = await _embyShowRepository.GetListAsync();
            var dbShow = dbShowList[0];
            dbShow.Name.ShouldBe("FBI");
        }
    }
}
