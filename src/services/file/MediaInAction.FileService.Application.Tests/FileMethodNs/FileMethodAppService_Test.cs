using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace MediaInAction.FileService.FileMethodNs;

public class FileMethodAppService_Test : FileServiceApplicationTestBase
{
    IFileMethodAppService _fileMethodAppService;

    public FileMethodAppService_Test()
    {
        _fileMethodAppService = GetRequiredService<IFileMethodAppService>();
    }

    [Fact]
    public async Task ShouldReturnAllFileMethods()
    {
        var fileMethods = await _fileMethodAppService.GetListAsync();

        fileMethods.ShouldNotBeEmpty();

        fileMethods.ShouldContain(x => x.Name == FileMethodNames.Move);
        fileMethods.ShouldContain(x => x.Name == FileMethodNames.UnCompress);
    }
}
