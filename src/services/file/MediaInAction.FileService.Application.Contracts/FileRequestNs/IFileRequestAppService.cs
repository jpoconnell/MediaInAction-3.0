using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.FileRequestNs;

public interface IFileRequestAppService : IApplicationService
{
    Task<FileRequestStartResultDto> StartAsync(string traktMethod, FileRequestStartDto input);

    Task<FileRequestDto> CompleteAsync(string traktMethod, FileRequestCompleteInputDto input);

    Task<bool> HandleWebhookAsync(string traktMethod, string payload);
}
