using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodNs;
using MediaInAction.FileService.FileRequestsNs;
using MediaInAction.FileService.FileRequestsNs.Dtos;

namespace MediaInAction.FileService.FileRequestNs;

public class FileRequestAppService : FileServiceAppService, IFileRequestAppService
{
    protected IFileRequestRepository _fileRequestRepository { get; }
    private readonly FileMethodResolver _fileMethodResolver;
    
    public FileRequestAppService(
        IFileRequestRepository fileRequestRepository,
        FileMethodResolver fileMethodResolver)
    {
        _fileRequestRepository = fileRequestRepository;
        _fileMethodResolver = fileMethodResolver;
    }


    public virtual async Task<FileRequestDto> CreateAsync(FileRequestCreateDto input)
    {
        var fileRequest = new FileRequest
        {
            Operation = input.Operation,
        };
        /*
        foreach (var fileRequestItem in input.Files
                     .Select(s => new FileRequestFile(
                         id: GuidGenerator.Create(),
                         //fileRequestId: fileRequest.Id,
                         server: s.Server,
                         filename: s.FileName,
                         directory: s.Directory)))
        {
            fileRequest.Files.Add(fileRequestItem);
        }
        */
        await _fileRequestRepository.InsertAsync(fileRequest);
        return ObjectMapper.Map<FileRequest, FileRequestDto>(fileRequest);
    }
    
    public virtual async Task<FileRequestStartResultDto> StartAsync(string fileType, FileRequestStartDto input)
    {
        FileRequest fileRequest =
            await _fileRequestRepository.GetAsync(input.FileRequestId, includeDetails: true);

        var fileService = _fileMethodResolver.Resolve(fileType);
        return await fileService.StartAsync(fileRequest, input);
    }
    
    public virtual async Task<FileRequestDto> CompleteAsync(string fileType, FileRequestCompleteInputDto input)
    {
        var fileService = _fileMethodResolver.Resolve(fileType);

        var fileRequest = await fileService.CompleteAsync(_fileRequestRepository, input.Token);
        return ObjectMapper.Map<FileRequest, FileRequestDto>(fileRequest);
    }

    public virtual async Task<bool> HandleWebhookAsync(string traktType, string payload)
    {
        var fileService = _fileMethodResolver.Resolve(traktType);

        await fileService.HandleWebhookAsync(payload);
        
        return true;
    }
}
