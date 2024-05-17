using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using Microsoft.Extensions.Logging;

namespace  MediaInAction.VideoService.DataMaintenanceNs;

public class ProcessMappedFiles : IProcessMappedFiles
{
    private readonly IFileEntryLibService _fileEntryService;
    private readonly ILogger<ProcessMappedFiles> _logger;
    private readonly IMovieService _movieService;
    private readonly IEpisodeService _episodeService;
    
    public ProcessMappedFiles( ILogger<ProcessMappedFiles> logger,
        IMovieService movieService,
        IEpisodeService episodeService,
        IFileEntryLibService fileEntryService
      )
    {
        _logger = logger;
        _episodeService = episodeService;
        _movieService = movieService;
        _fileEntryService = fileEntryService;
    }
    
    public async Task Process()
    {
        _logger.LogInformation("Running MapFiles Service");
        var returnList = new List<FileEntryDto>();
        
        var fileEntryDtoList = await _fileEntryService.GetMapped(false);
        var startCnt = fileEntryDtoList.Count;
        
        foreach (var fileEntryDto in fileEntryDtoList)
        {
            fileEntryDto.Updates = 0;
            if (fileEntryDto.MediaType == MediaType.Episode)
            {
                var episodeDto = await _episodeService.GetByIdAsync(fileEntryDto.Link );
     
                if (fileEntryDto.ListName == ListType.Compressed)
                {
                    if ((episodeDto.EpisodeStatus == MediaStatus.New)
                        || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                        || (episodeDto.EpisodeStatus == MediaStatus.Indexed))
                    {
                        episodeDto.EpisodeStatus = MediaStatus.Compressed;
                        await _episodeService.UpdateAsync(episodeDto);
                    }
                }
                if (fileEntryDto.ListName == ListType.Uncompressed)
                {
                    if ((episodeDto.EpisodeStatus == MediaStatus.New)
                        || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                        || (episodeDto.EpisodeStatus == MediaStatus.Indexed))
                    {
                        episodeDto.EpisodeStatus = MediaStatus.UnCompressed;
                        await _episodeService.UpdateAsync(episodeDto);
                    }
                }
                if (fileEntryDto.ListName == ListType.Current)
                {
                    if ((episodeDto.EpisodeStatus == MediaStatus.New)
                        || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                        || (episodeDto.EpisodeStatus == MediaStatus.Indexed))
                    {
                        episodeDto.EpisodeStatus = MediaStatus.Complete;
                        await _episodeService.UpdateAsync(episodeDto);
                    }
                }
            }
            else  // Try finding it in movies
            {
                if (fileEntryDto.SeriesLink != Guid.Empty)
                {
                    var movieDto = await _movieService.GetByIdAsync(fileEntryDto.SeriesLink );
                    if (movieDto != null)
                    {
                    
                    }
                }
            }
        }
    }
}