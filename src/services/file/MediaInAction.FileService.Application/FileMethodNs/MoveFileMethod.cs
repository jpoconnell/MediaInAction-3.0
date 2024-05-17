using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.FileService.FileRequestNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileMethodNs;

[ExposeServices(typeof(IFileMethod), typeof(MoveFileMethod))]
public class MoveFileMethod : IFileMethod
{
    public string Name => FileMethodNames.Move;

    public Task<FileRequestStartResultDto> StartAsync(FileRequest fileRequest, FileRequestStartDto input)
    {
        
        return Task.FromResult(new FileRequestStartResultDto
        {
            CheckoutLink = input.ReturnUrl + "?token=" + input.FileRequestId
        });
    }

    public async Task<FileRequest> CompleteAsync(IFileRequestRepository fileRequestRepository, string token)
    {
        var fileRequest = await fileRequestRepository.GetAsync(Guid.Parse(token));

        fileRequest.SetAsCompleted();

        return await fileRequestRepository.UpdateAsync(fileRequest);
    }

    public Task HandleWebhookAsync(string payload)
    {
        return Task.CompletedTask;
    }
}