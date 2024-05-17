using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.FileEntryNs;
using MongoDB.Driver.Linq;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.FileService.MongoDB.FileEntriesNs;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(FileServiceTestConsts.CollectionDefinitionName)]
public class FileEntryRepositoryTests : FileServiceMongoDbTestBase
{
    private readonly IRepository<FileEntry, Guid> _fileEntryRepository;

    public FileEntryRepositoryTests()
    {
        _fileEntryRepository = GetRequiredService<IRepository<FileEntry, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));       
                var fileEntry = await (await _fileEntryRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync(u => u.CreationTime < daysAgo30);

                //Assert
                fileEntry.ShouldNotBeNull();
        });
    }
}
