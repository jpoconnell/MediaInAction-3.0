using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntryManagerUnitTests : VideoServiceDomainTestBase
{
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileEntryManager _fileEntryManager;

    public FileEntryManagerUnitTests()
    {
        _fileEntryRepository = GetRequiredService<IFileEntryRepository>();
        _fileEntryManager = GetRequiredService<FileEntryManager>();
    }
    
    [Fact]
    public async Task Should_CreateFileEntryAsync()
    {
        
        var fileEntryCreate = new FileEntryCreateDto
        {
            ExternalId = "",
            Server = "feederbox1",
            FileName = "testfile",
            Directory = "/etc/feeds",
            Size = 20,
            Extn = "mkv",
            ListName = "Current",
            Sequence = 1
        };
        
        var createdFileEntry = await _fileEntryManager.CreateAsync(fileEntryCreate);
        createdFileEntry.ShouldNotBeNull();
    }
}
