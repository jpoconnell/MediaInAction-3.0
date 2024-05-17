using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.ParserNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.FileServicesNs;

public class FileMove : IFileMove
{
    private readonly ILogger<FileMove> _logger;
    private readonly IFileEntryLibService _fileEntryService;
    private readonly IParserService _parserService;
    private readonly IEpisodeService _episodeService;
    
    public FileMove(
        ILogger<FileMove> logger,
        IFileEntryLibService fileEntryService,
        IParserService parserService,
        IEpisodeService episodeService
        )
    {
        _logger = logger;
        _fileEntryService = fileEntryService;
        _episodeService = episodeService;
        _parserService = parserService;
    }
    
    public async Task GetMoveList()
    {
        _logger.LogInformation("Running MapFiles Service");
        var returnList = new List<FileEntryDto>();
       
        var episodeDtoList = await _episodeService.EpisodeFilesToMove();
        var startCnt = episodeDtoList.Count;
        var toMove = true;
        if (startCnt > 0)
        {
            foreach (var episodeDto in episodeDtoList)
            {
                var fileEntryDtoList = await _fileEntryService.GetByLink(episodeDto.Id);

                if (fileEntryDtoList.Count > 0)
                {
                    foreach (var fileEntryDto in fileEntryDtoList)
                    {
                        var updates = 0;
                        if (fileEntryDto.ListName == ListType.Current)
                        {
                            fileEntryDto.FileStatus = FileStatus.ToWatch;
                            updates++;
                        }
                        if (fileEntryDto.ListName == ListType.Uncompressed)
                        {
                            fileEntryDto.FileStatus = FileStatus.ToMove;
                            updates++;
                        }
                        
                        if (updates > 0)
                        {
                            await _fileEntryService.UpdateAsync(fileEntryDto );
                        }
                    }
                }
            }
        }
    }
}