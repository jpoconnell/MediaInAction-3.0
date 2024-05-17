using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace MediaInAction.FileService.FileRequestNs;

public class FileRequestAppService_Test : FileServiceApplicationTestBase
{
    IFileRequestAppService _fileRequestAppService;

    public FileRequestAppService_Test()
    {
        _fileRequestAppService = GetRequiredService<IFileRequestAppService>();
    }
    
}
