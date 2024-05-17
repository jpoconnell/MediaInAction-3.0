using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.FileService.FileRequestNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileMethodNs;

public interface IFileMethod : ITransientDependency
{
    string Name { get; }

    public Task<FileRequestStartResultDto> StartAsync(FileRequest fileRequest, FileRequestStartDto input);

    public Task<FileRequest> CompleteAsync(IFileRequestRepository fileRequestRepository, string token);

    public Task HandleWebhookAsync(string payload);
}