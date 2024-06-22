using System;
using System.Threading.Tasks;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntryManager : DomainService
{
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly ILogger<FileEntryManager> _logger;
    
    public FileEntryManager(IFileEntryRepository fileEntryRepository,
        ILogger<FileEntryManager> logger,
        IDistributedEventBus distributedEventBus)
    {
        _fileEntryRepository = fileEntryRepository;
        _logger = logger;
        _distributedEventBus = distributedEventBus;
    }

    public async Task<FileEntry> CreateAsync(FileEntryCreateDto fileEntryCreateDto)
    {
        try
        {
            var myExternalId = "";
            if (fileEntryCreateDto.ExternalId != null)
            {
                myExternalId = fileEntryCreateDto.ExternalId;
            }

            // Create new fileEntry
            var newFileEntry = new FileEntry(
                id: GuidGenerator.Create(),
                externalId: myExternalId,
                server: fileEntryCreateDto.Server,
                fileName: fileEntryCreateDto.FileName,
                directory: fileEntryCreateDto.Directory,
                extn: fileEntryCreateDto.Extn,
                size: fileEntryCreateDto.Size,
                listName: 0,
                sequence: fileEntryCreateDto.Sequence,
                status: FileStatus.New,
                isMapped: false
            );

            var dbFileEntry = await _fileEntryRepository.FindFileEntry(newFileEntry.Server,
                newFileEntry.Directory,
                newFileEntry.FileName,
                newFileEntry.ListName);

            if (dbFileEntry == null)
            {
                var createdFileEntry = await _fileEntryRepository.InsertAsync(newFileEntry, true);
                return createdFileEntry;
            }
            else
            {
                var update = 0;
                if (dbFileEntry.FileStatus != newFileEntry.FileStatus)
                {
                    dbFileEntry.FileStatus = newFileEntry.FileStatus;
                    update++;
                }

                if (update > 0)
                {
                    await _fileEntryRepository.UpdateAsync(dbFileEntry);
                }

                return dbFileEntry;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public async Task<FileEntry> CreateFileEntryAsync(
        Guid? externalId,
        string server,
        string directory,
        string extn,
        long size,
        string fileName,
        ListType listName,
        int sequence,
        FileStatus fileStatus,
        bool mapped = false
    )
    {
        try
        {
            var myExternalId = "";
            if (externalId == null)
            {
                myExternalId = externalId.ToString();

            }

            // Create new fileEntry
            FileEntry newFileEntry = new FileEntry(
                id: GuidGenerator.Create(),
                externalId: myExternalId,
                server: server,
                fileName: fileName,
                directory: directory,
                extn: extn,
                size: size,
                listName: listName,
                sequence: sequence,
                status: fileStatus,
                isMapped: mapped
            );

            var dbFileEntry = await _fileEntryRepository.FindFileEntry(newFileEntry.Server,
                newFileEntry.Directory,
                newFileEntry.FileName,
                newFileEntry.ListName);

            if (dbFileEntry == null)
            {
                var createdFileEntry = await _fileEntryRepository.InsertAsync(newFileEntry, true);
                return createdFileEntry;
            }
            else
            {
                var update = 0;
                if (dbFileEntry.FileStatus != fileStatus)
                {
                    dbFileEntry.FileStatus = fileStatus;
                    update++;
                }

                if (update > 0)
                {
                    await _fileEntryRepository.UpdateAsync(dbFileEntry);
                }

                return dbFileEntry;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<FileEntry> AcceptFileEntryAsync(
        Guid externalFileId, 
        string server,
        string directory,
        string extn,
        long size,
        string fileName,
        ListType listName,
        int sequence,
        FileStatus fileStatus)
    {
        _logger.LogInformation("AcceptFileEntryAsync");

        var newFileEntry = await CreateFileEntryAsync(externalFileId,
            server, 
            directory, 
            extn,
            size,
            fileName,
            listName,
            sequence,
            fileStatus);
        
        return newFileEntry;
    }

    public async Task UpdateStatusAsync(FileEntry fileEntry)
    {
        var dbFileEntry = await _fileEntryRepository.GetAsync(fileEntry.Id);
        
        if (dbFileEntry.FileStatus != fileEntry.FileStatus)
        {
            dbFileEntry.FileStatus = fileEntry.FileStatus;
            await _fileEntryRepository.UpdateAsync(fileEntry, true);
            await SendUpdateStatusEvent(dbFileEntry);
        }
    }
    
    public async Task SendUpdateStatusEvent(FileEntry fileEntry)
    {
        var dbFileEntry = await _fileEntryRepository.GetAsync(fileEntry.Id);
        if (fileEntry.FileStatus != dbFileEntry.FileStatus)
        {
            dbFileEntry.FileStatus = FileStatus.Mapped;
            if (dbFileEntry.ExternalId.IsNullOrEmpty())
            {}
            await _distributedEventBus.PublishAsync(new FileEntryStatusEto
            {
                FileEntryId = dbFileEntry.ExternalId,
                FileName = dbFileEntry.Server,
                Server = dbFileEntry.Server,
                ListName = dbFileEntry.ListName,
                FileStatus = dbFileEntry.FileStatus
            });
            _logger.LogInformation("Sent FileEntry Status Change");
        }
    }

    public async Task<FileEntry> FileEntryStatusAsync(Guid fileId, 
        string server, 
        string fileName, 
        ListType listNamw, 
        FileStatus fileStatus)
    {
        var fileEntry = await _fileEntryRepository.GetByExternalId(fileId);
        if (fileEntry.FileStatus != fileStatus)
        {
            fileEntry.FileStatus = fileStatus;
            await _fileEntryRepository.UpdateAsync(fileEntry, true);
            
            _logger.LogInformation("FileEntry Status Updated:" + fileEntry.FileName);
        }
        return fileEntry;
    }
}
