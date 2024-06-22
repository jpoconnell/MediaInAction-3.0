using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.MediaFoldersNs;

public class MediaFolderManager : DomainService
{
    private readonly IMediaFolderRepository _mediaFolderRepository;
    private ILogger<MediaFolderManager> _logger;
    
    public MediaFolderManager(
        IMediaFolderRepository mediaFolderRepository,
        ILogger<MediaFolderManager> logger
    )
    {
        _mediaFolderRepository = mediaFolderRepository;
        _logger = logger;
    }

    public async Task<MediaFolder> CreateMediaFolderAsync(
        string server,
        string name,
        string id
    )
    {
        // Create new mediaFolder
        MediaFolder folder = new MediaFolder
        {
            Name = name,
            Server = server
        };

        try
        {
            var createdMediaFolder = await _mediaFolderRepository.InsertAsync(folder, true);
            return createdMediaFolder;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
