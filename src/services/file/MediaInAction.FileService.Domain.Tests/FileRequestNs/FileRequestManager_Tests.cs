using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MediaInAction.FileService.FileRequestNs;
public class FileRequestManager_Tests : FileServiceDomainTestBase
{
    private readonly FileRequestManager _fileRequestManager;
    private readonly IFileRequestRepository _fileRequestRepository;
    
    public FileRequestManager_Tests()
    {
        _fileRequestManager = GetRequiredService<FileRequestManager>();
        _fileRequestRepository =  GetRequiredService<IFileRequestRepository>();
    }

    [Fact]
    public async Task Should_Set_Status_FileEntry()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
            var fileRequest = await _fileRequestRepository.FirstOrDefaultAsync();
            fileRequest.State = FileRequestState.Completed;
            await _fileRequestRepository.UpdateAsync(fileRequest);
        });

        var dbFileRequestList = await _fileRequestRepository.GetListAsync();
        var dbFileRequest = dbFileRequestList[0];
        dbFileRequest.State.ShouldBe(FileRequestState.Completed);
    }
}
