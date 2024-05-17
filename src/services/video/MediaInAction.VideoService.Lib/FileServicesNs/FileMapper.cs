using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.FileServicesNs;

public class FileMapper : IFileMapper
{
    private readonly ILogger<FileMapper> _logger;
    private readonly IFileEntryLibService _fileEntryService;
    private readonly IParserService _parserService;
    private readonly IEpisodeService _episodeService;
    private readonly IMovieService _movieService;
    private readonly IToBeMappedService _toBeMappedService;

    public FileMapper(
        ILogger<FileMapper> logger,
        IFileEntryLibService fileEntryService,
        IParserService parserService,
        IEpisodeService episodeService,
        IToBeMappedService toBeMappedService,
        IMovieService movieService
        )
    {
        _logger = logger;
        _fileEntryService = fileEntryService;
        _parserService = parserService;
        _episodeService = episodeService;
        _toBeMappedService = toBeMappedService;
        _movieService = movieService;
    }

    public async Task MapFiles()
    {
        _logger.LogInformation("Running MapFiles Service");
        var returnList = new List<FileEntryDto>();
       
        var fileEntryDtoList = await _fileEntryService.GetMapped(false);
        var startCnt = fileEntryDtoList.Count;
        var cnt = 0;
        _logger.LogInformation("Total File Entries:" + startCnt.ToString());
        foreach (var fileEntryDto in fileEntryDtoList)
        {
            fileEntryDto.Updates = 0;
           
            if ((fileEntryDto.FileStatus == FileStatus.New) && 
                (fileEntryDto.Directory.Length > 1) 
                && (fileEntryDto.Size > 1000))
            {
                try
                {
                    if ((fileEntryDto.Directory.Contains("thumb")) || (fileEntryDto.Size < 100000))
                    {
                        fileEntryDto.IsMapped = true;
                        fileEntryDto.FileStatus = FileStatus.ToDelete;
                        fileEntryDto.Updates++;
                    }
                    else
                    {
                        var parser = await _parserService.MapProcess(fileEntryDto);
                        if (fileEntryDto.MediaType != parser.MediaType)
                        {
                            fileEntryDto.MediaType = parser.MediaType;
                            fileEntryDto.Updates++;
                        }
                        if (parser.MediaType == MediaType.Other)
                        {
                            await this.MapFilesToMovies(fileEntryDto, parser);
                        }

                        if (parser.MediaType == MediaType.Movie)
                        {
                            await this.MapFilesToMovies(fileEntryDto, parser);
                        }

                        if (parser.MediaType == MediaType.Episode)
                        {
                            await this.MapFilesToEpisodes(fileEntryDto, parser);
                        }
                    
                        if ((fileEntryDto.Link != Guid.Empty) && (fileEntryDto.IsMapped == false))
                        {
                            fileEntryDto.IsMapped = true;
                            fileEntryDto.Updates++;
                        }
                        cnt++;
                        if (cnt > 300)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("Already"))
                    {
                        _logger.LogDebug(ex.Message);
                    }
                }
            }
            else
            {
                if (fileEntryDto.Size < 1000)
                {
                    fileEntryDto.IsMapped = true;
                    fileEntryDto.Updates++;
                }
                    
            }
        }
        foreach (var fileEntryDto in fileEntryDtoList)
        {
            if (fileEntryDto.Updates > 0)
            {
                returnList.Add(fileEntryDto);
            }
        }

        if (returnList.Count > 0)
        {
            await UpdateFiles(returnList);
        }
        _logger.LogInformation("FileEntries Not Mapped:" + startCnt.ToString());
        _logger.LogInformation("FileEntries To Be Updated:" + returnList.Count.ToString());
    }
    
    private async Task<bool> MapFilesToEpisodes(FileEntryDto fileEntry, ParserDto parser)
    {
        fileEntry.MediaType = MediaType.Episode;

        if ((fileEntry.SeriesLink == Guid.Empty) && (parser.SeriesLink != Guid.Empty))
        {
            fileEntry.SeriesLink = parser.SeriesLink;
            fileEntry.Updates++;
        }

        if ((fileEntry.Link == Guid.Empty) && (parser.Link != Guid.Empty))
        {
            fileEntry.Link = parser.Link;
            fileEntry.Updates++;
        }
        
        if (fileEntry.Link != Guid.Empty)
        {
            var episodeDto = await _episodeService.GetByIdAsync(fileEntry.Link );
    
            if (fileEntry.ListName == ListType.Compressed)
            {
                if ((episodeDto.EpisodeStatus == MediaStatus.New)
                    || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                    || (episodeDto.EpisodeStatus == MediaStatus.Indexed))
                {
                    episodeDto.EpisodeStatus = MediaStatus.Compressed;
                    await _episodeService.UpdateAsync(episodeDto);
                }
            }

            if (fileEntry.ListName == ListType.Uncompressed)
            {
                if ((episodeDto.EpisodeStatus == MediaStatus.New) 
                    || (episodeDto.EpisodeStatus == MediaStatus.Indexed)
                    || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                    || (episodeDto.EpisodeStatus == MediaStatus.Compressed))
                {
                    episodeDto.EpisodeStatus = MediaStatus.UnCompressed;
                    await _episodeService.UpdateAsync(episodeDto);
                    fileEntry.FileStatus = FileStatus.ToMove;
                    fileEntry.CleanFileName = parser.OutFullPath;
                    fileEntry.Updates++;
                }
            }
            if (fileEntry.ListName == ListType.Current)
            {
                if ((episodeDto.EpisodeStatus == MediaStatus.New) 
                    || (episodeDto.EpisodeStatus == MediaStatus.Indexed)
                    || (episodeDto.EpisodeStatus == MediaStatus.Torrent)
                    || (episodeDto.EpisodeStatus == MediaStatus.Move)
                    || (episodeDto.EpisodeStatus == MediaStatus.UnCompressed))
                {
                    episodeDto.EpisodeStatus = MediaStatus.Complete;
                    await _episodeService.UpdateAsync(episodeDto);
                    fileEntry.FileStatus = FileStatus.ToWatch;
                    fileEntry.Updates++;
                }
            }
            if (fileEntry.Resolution.IsNullOrEmpty())
            {
                if (!parser.Resolution.IsNullOrEmpty())
                {
                    fileEntry.Resolution = parser.Resolution;
                    fileEntry.Updates++;
                }
            }
        }
        else 
        {
            if (parser.SeriesLink != Guid.Empty)
            {
                await _episodeService.CreateAsync(parser);
            }
            if (parser.ToBeMapped == true)
            {
                if (parser.SeriesName.Length > 1)
                {
                    parser.SeriesName = parser.SeriesName.ToLower();
                    await _toBeMappedService.CreateToBeMappedASync(parser.SeriesName);
                }
            }
            return true;
        }
        return true;
    }

    private async Task<bool> MapFilesToMovies(FileEntryDto fileEntry, ParserDto parser)
    {
        var result = false;
        try
        {
            var updates = 0;
            if ((fileEntry.Link == Guid.Empty) && (parser.SeriesLink != Guid.Empty))
            {
                fileEntry.Link = parser.Link;
                fileEntry.IsMapped = true;
                fileEntry.Updates++;
                parser.ToBeMapped = false;
                updates++;
            }
            if (fileEntry.Link != Guid.Empty)
            {
                var movieDto = await _movieService.GetByIdAsync(fileEntry.Link );

                if (fileEntry.ListName == ListType.Compressed)
                {
                    if ((movieDto.MovieStatus == MediaStatus.New)
                        || (movieDto.MovieStatus == MediaStatus.Indexed)
                        || (movieDto.MovieStatus == MediaStatus.Compressed))
                    {
                        movieDto.MovieStatus = MediaStatus.Compressed;
                        await _movieService.UpdateAsync(movieDto);
                        updates++;
                    }
                }

                if (fileEntry.ListName == ListType.Uncompressed)
                {
                    if ((movieDto.MovieStatus == MediaStatus.New)
                        || (movieDto.MovieStatus == MediaStatus.UnCompressed)
                        || (movieDto.MovieStatus == MediaStatus.Torrent)
                        || (movieDto.MovieStatus == MediaStatus.UnCompressed))
                    {
                        movieDto.MovieStatus = MediaStatus.Move;
                        await _movieService.UpdateAsync(movieDto);
                        updates++;
                    }
                }
                if (fileEntry.ListName == ListType.Current)
                {
                    if ((movieDto.MovieStatus == MediaStatus.New)
                        || (movieDto.MovieStatus == MediaStatus.New)
                        || (movieDto.MovieStatus == MediaStatus.Torrent)
                        || (movieDto.MovieStatus == MediaStatus.Indexed)
                        || (movieDto.MovieStatus == MediaStatus.Move))
                    {
                        movieDto.MovieStatus = MediaStatus.Complete;
                        await _movieService.UpdateAsync(movieDto);
                        updates++;
                    }
                }

                if (fileEntry.Size < 1000)
                {
                    
                }
                
            }
            else
            {
                if (parser.ToBeMapped == true)
                {
                    if (parser.SeriesName.Length > 1)
                    {
                        var myAlias = parser.SeriesName.ToLower();
                        await _toBeMappedService.CreateToBeMappedASync(myAlias);
                    }
                }
            }
            if (updates > 0)
            {
                await _fileEntryService.UpdateAsync(fileEntry);
            }  
            
        }
        catch (Exception ex)
        {
            if(!ex.Message.Contains("Already"))
            {
                _logger.LogDebug("MapFilesToMovies" +ex.Message);
            }
        }         
        return result;
    }
    
    
    private async Task UpdateFiles(List<FileEntryDto> fileList)
    {
        _logger.LogInformation("Running UpdateFiles");
        foreach (var fileEntryDto in fileList)
        {
            if ((fileEntryDto.Link != Guid.Empty) && (fileEntryDto.IsMapped == false))
            {
                fileEntryDto.IsMapped = true;
            }
            
            await _fileEntryService.UpdateAsync(fileEntryDto);
        }
    }
}
