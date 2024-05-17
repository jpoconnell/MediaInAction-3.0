using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.FileService.FileEntryNs;
public class FileEntryManager_Tests : FileServiceDomainTestBase
{
    private readonly FileEntryManager _fileEntryManager;
    private readonly IFileEntryRepository _fileEntryRepository;
    
    public FileEntryManager_Tests()
    {
        _fileEntryManager = GetRequiredService<FileEntryManager>();
        _fileEntryRepository =  GetRequiredService<IFileEntryRepository>();
    }

    [Fact]
    public async Task Should_Set_Status_FileEntry()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            var fileEntry = await _fileEntryRepository.FirstOrDefaultAsync();
            fileEntry.FileStatus = FileStatus.New;
            await _fileEntryRepository.UpdateAsync(fileEntry);
        });

        var  dbFileEntryList = await _fileEntryRepository.GetListAsync();
        var dbFileEntry = dbFileEntryList[0];
        dbFileEntry.FileStatus.ShouldBe(FileStatus.New);
    }
}
