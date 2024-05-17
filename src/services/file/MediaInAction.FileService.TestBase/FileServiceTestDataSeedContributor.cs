using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntryNs;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService
{
    public class FileServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        
        private readonly FileEntryManager _fileEntryManager;
        private readonly IFileEntryRepository _fileEntryRepository;
        private readonly FileRequestManager _fileRequestManager;
        private readonly IFileRequestRepository _fileRequestRepository;
        private readonly TestData _testData;
        
        public Task SeedAsync(DataSeedContext context)
        {
            SeedTestFileServiceAsync();
            return Task.CompletedTask;
        }

        private async Task SeedTestFileServiceAsync()
        {
            await _fileEntryManager.CreateAsync(
                "server1",
                _testData.FileName1,
                "FileName1",
                ".rar",
                0,
                1,
                ListType.Compressed
            );
            
            await _fileEntryManager.CreateAsync(
                "server1",
                _testData.FileName2,
                "FileName1",
                ".rar",
                2,
                1,
                ListType.Compressed
            );
            
            await _fileEntryManager.CreateAsync(
                "server1",
                _testData.FileName3,
                "FileName3",
                ".mkv",
                3,
                1,
                ListType.Uncompressed
            );
            
        }
    }
}