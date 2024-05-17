using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using Xunit;

namespace MediaInAction.FileService.FileEntryNs;

public class FileEntryAppService_Test : FileServiceApplicationTestBase
{
    IFileEntryAppService _fileEntryAppService;

    public FileEntryAppService_Test()
    {
        _fileEntryAppService = GetRequiredService<IFileEntryAppService>();
    }

    [Fact]
    public async Task ShouldReturnAllFileEntries()
    {
        var fileEntries = await _fileEntryAppService.GetListAsync();

        //fileEntries.Count.S();
        
    }
}
