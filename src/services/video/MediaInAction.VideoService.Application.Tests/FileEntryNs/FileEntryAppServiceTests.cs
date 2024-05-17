using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntryAppServiceTests : VideoServiceApplicationTestBase
{
    private readonly IFileEntryAppService _fileEntryAppService;

    public FileEntryAppServiceTests()
    {
        _fileEntryAppService = GetRequiredService<IFileEntryAppService>();
    }

    [Fact]
    public async Task Get_Always_ReturnsAllFileEntry()
    {
        //Act
        var filter = new GetFileEntriesInput();
        var result = await _fileEntryAppService.GetListPagedAsync(filter);

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);

    }
}
