using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.Shared.Domain.Enums;
using Shouldly;
using Xunit;

namespace MediaInAction.FileService.MongoDB.FileRequestNs;

    public class FileRequestRepository_Tests : FileServiceMongoDbTestBase
    {
        private readonly IFileRequestRepository _fileRequestRepository;
        public FileRequestRepository_Tests()
        {
            _fileRequestRepository = GetRequiredService<IFileRequestRepository>();
        }

        [Fact]
        public async Task Should_Insert_File_Request()
        {
            var id = Guid.NewGuid();
            var fileRequest = new FileRequest(id, FileOperation.Move);

            await _fileRequestRepository.InsertAsync(fileRequest);

            var inserted = await _fileRequestRepository.GetAsync(id);

            inserted.Id.ShouldNotBe(Guid.Empty);
        }
    }
