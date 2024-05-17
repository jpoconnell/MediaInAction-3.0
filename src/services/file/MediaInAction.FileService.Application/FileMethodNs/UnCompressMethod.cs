using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileRequestNs;
using MediaInAction.FileService.FileRequestNs.Dtos;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileMethodNs;

[ExposeServices(typeof(IFileMethod), typeof(UnCompressMethod))]
public class UnCompressMethod : IFileMethod
{
   
    private readonly FileRequestDomainService _fileRequestDomainService;
    public string Name => FileMethodNames.UnCompress;
    
    public Task<FileRequestStartResultDto> StartAsync(FileRequest paymentRequest, FileRequestStartDto input)
    {
        throw new NotImplementedException();
    }

    public Task<FileRequest> CompleteAsync(IFileRequestRepository paymentRequestRepository, string token)
    {
        throw new NotImplementedException();
    }

    public Task HandleWebhookAsync(string payload)
    {
        throw new NotImplementedException();
    }
}