using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using Microsoft.Extensions.Logging;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntryLibService: IFileEntryLibService
{
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileEntryManager _fileEntryManager;
    private readonly ILogger<FileEntryLibService> _logger;
    
    public FileEntryLibService(
        FileEntryManager fileEntryManager,
        IFileEntryRepository fileEntryRepository,
        ILogger<FileEntryLibService> logger )
    {
        _fileEntryRepository = fileEntryRepository;
        _fileEntryManager = fileEntryManager;
        _logger = logger;
    }
    
    public async Task<List<FileEntryDto>> GetMapped()
    {
        var fileEntryDtoList = new List<FileEntryDto>();
        ISpecification<FileEntry> specification = Specifications.SpecificationFactory.Create("l:");
        var fileList = await _fileEntryRepository.GetFileEntriesAsync(specification);
        foreach (var fileEntry in fileList)
        {
            var fileEntryDto = new FileEntryDto
            {
                Directory = fileEntry.Directory,
                MediaType = fileEntry.MediaType,
                Extn = fileEntry.Extn,
                ListName = fileEntry.ListName,
                FileName = fileEntry.FileName,
                Id = fileEntry.Id,
                Server = fileEntry.Server,
                FileStatus = fileEntry.FileStatus,
                SeriesLink = fileEntry.SeriesLink,
                Link = fileEntry.EpisodeLink
            };
            fileEntryDto.FileName = fileEntry.FileName;
            fileEntryDtoList.Add(fileEntryDto);
        }

        return fileEntryDtoList;
    }

    public async Task<List<FileEntryDto>> GetByLink(Guid episodeId)
    {
        ISpecification<FileEntry> specification = Specifications.SpecificationFactory.Create("l:");
        var fileList = await _fileEntryRepository.GetFileEntriesAsync(specification);
        var fileEntryDtoList = new List<FileEntryDto>();
        
        foreach (var file in fileList)
        {
            var fileOut = new FileEntryDto();
            fileOut.Directory = file.Directory;
            fileOut.Server = file.Server;
            fileOut.FileName = file.FileName;
            fileOut.Id = file.Id;
            fileOut.ListName = file.ListName;
            fileOut.Extn = Path.GetExtension(fileOut.FileName);
            fileOut.SeriesLink = file.SeriesLink;
            fileOut.Size = file.Size;
            fileEntryDtoList.Add(fileOut);
        }

        return fileEntryDtoList;
    }

    public async Task<List<FileEntryDto>> GetMapped(bool isMapped)
    {
        ISpecification<FileEntry> specification = Specifications.SpecificationFactory.Create("m:T");
        var fileList = await _fileEntryRepository.GetFileEntriesAsync(specification);
        var fileEntryDtoList = new List<FileEntryDto>();
        return fileEntryDtoList;
    }

    public async Task UpdateAsync(FileEntryDto fileEntryDto)
    {
        try
        {
            var fileEntry = await _fileEntryRepository.FindFileEntry(fileEntryDto.Server,
                fileEntryDto.Directory,fileEntryDto.FileName,fileEntryDto.ListName);

            var updates = 0;
            if (fileEntry != null)
            {
                if (fileEntryDto.SeriesLink != Guid.Empty)
                {
                    fileEntry.SeriesLink = fileEntryDto.SeriesLink;
                    updates++;
                }

                if (fileEntryDto.ListName != fileEntry.ListName)
                {
                    fileEntry.ListName = fileEntryDto.ListName;
                    updates++;
                }

                if (fileEntryDto.Link != Guid.Empty)
                {
                    fileEntry.EpisodeLink = fileEntryDto.Link;
                    fileEntry.FileStatus = FileStatus.Mapped;
                    updates++;
                }

                if (fileEntryDto.MediaType != MediaType.Other)
                {
                    fileEntry.MediaType = fileEntryDto.MediaType;
                    updates++;
                }

                if ((fileEntry.FileStatus == FileStatus.Mapped) && (fileEntryDto.FileStatus != fileEntry.FileStatus))
                {
                    if (fileEntryDto.FileStatus != fileEntry.FileStatus)
                    {
                        fileEntry.FileStatus = fileEntryDto.FileStatus;
                        updates++;
                    }
                }
                
                if (updates > 0)
                {
                    await _fileEntryManager.SendUpdateStatusEvent( fileEntry);
                    await _fileEntryRepository.UpdateAsync(fileEntry, true);
                } 
            }
            else
            {
                _logger.LogDebug("FileEntry not found for: " + fileEntryDto.Server + ";" + 
                                 fileEntryDto.Directory + ";"  + fileEntryDto.FileName);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("FileEntryService.UpdateAsync:" + ex.Message);
        }
    }

    public async Task<List<FileEntryDto>> GetUnMapped()
    {
        var fileEntryDtoList = new List<FileEntryDto>();
        ISpecification<FileEntry> specification = Specifications.SpecificationFactory.Create("m:");
        var fileList = await _fileEntryRepository.GetFileEntriesAsync(specification);
        foreach (var fileEntry in fileList)
        {
            var fileEntryDto = new FileEntryDto
            {
                Directory = fileEntry.Directory,
                MediaType = fileEntry.MediaType,
                ListName = fileEntry.ListName,
                Id = fileEntry.Id,
                Server = fileEntry.Server,
                FileStatus = fileEntry.FileStatus,
                SeriesLink = fileEntry.SeriesLink,
                Link = fileEntry.EpisodeLink,
                Extn = fileEntry.Extn,
                Size = fileEntry.Size,
                FileName = fileEntry.FileName
            };
            fileEntryDtoList.Add(fileEntryDto);
        }

        return fileEntryDtoList;
    }
}

