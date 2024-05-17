using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MediaFolderNs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace MediaInAction.EmbyService.FolderServices;

public class FolderService : IFolderService
{
    private readonly ILogger _logger;
    private readonly MediaFolderManager _mediaFolderManager;
    private readonly IMediaFolderRepository _mediaFolderRepository;
    
    public FolderService(
        IMediaFolderRepository  mediaFolderRepository,
        MediaFolderManager mediaFolderManager
    )
    {
        _logger = NullLogger.Instance;
        _mediaFolderRepository = mediaFolderRepository;
        _mediaFolderManager = mediaFolderManager;
    }

    public async Task UpdateAddFromDto(EmbyFolderDto folder)
    {
        try 
        { 
            await CreateUpdateFolder(folder);
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }

    private async Task CreateUpdateFolder(EmbyFolderDto folder)
    {
        try
        {
            var dbShow = await _mediaFolderRepository.GetByServerNameAsync(folder.Server, folder.Name);
            if (dbShow == null)
            {
                var createdFolder = await _mediaFolderManager.CreateMediaFolderAsync(folder.Server, folder.Name, folder.Id);
            }
           
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
    }
}

