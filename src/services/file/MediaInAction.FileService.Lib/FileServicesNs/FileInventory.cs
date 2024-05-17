using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediaInAction.FileService.FileEntryNs;
using MediaInAction.FileService.Lib.Config;
using MediaInAction.FileService.Lib.FileServicesNs.Dtos;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.Lib.FileServicesNs;

public class FileInventory : IFileInventory
{
    private readonly ILogger<FileInventory> _logger;
    private readonly IFileEntryLibService _fileEntryService;
    private readonly FileServicesConfiguration _fileServicesConfiguration;
    private string _currentLocation;
    private static string[] mediaTypes = {"divx", "mp4", "mkv", "avi"};
    private static string[] compressType = { "rar", "zip" };

    public FileInventory(
        ILogger<FileInventory> logger,
        IFileEntryLibService fileEntryService,
        FileServicesConfiguration fileServicesConfiguration)
    {
        _logger = logger;
        _fileEntryService = fileEntryService;
        _fileServicesConfiguration = fileServicesConfiguration;
    }
    
    public async Task<bool> GetFiles()
    {
        _logger.LogInformation("GetFiles service started");
        _currentLocation = System.Net.Dns.GetHostName();
        var result = false;
        try
        {
            var mediaLocation = new MediaLocationDto();
            mediaLocation.Directory = _fileServicesConfiguration.UnCompressedFolder;
            mediaLocation.ListName = ListType.Uncompressed;
            await this.ReadFilesFromDirectory(mediaLocation);
            
            mediaLocation.Directory = _fileServicesConfiguration.CompressedFolder;
            mediaLocation.ListName = ListType.Compressed;
            await this.ReadFilesFromDirectory(mediaLocation);
            
            mediaLocation.Directory = _fileServicesConfiguration.LibraryTv;
            mediaLocation.ListName = ListType.Current;
            await this.ReadFilesFromDirectory(mediaLocation);
            
            mediaLocation.Directory = _fileServicesConfiguration.LibraryMovie;
            mediaLocation.ListName = ListType.Current;
            await this.ReadFilesFromDirectory(mediaLocation);
        }
        catch (Exception ex)
        {
            result = false;
            _logger.LogDebug("FileInventory Error:" + ex.Message);
        }
        return result;
    }

    private async Task ReadFilesFromDirectory(MediaLocationDto mediaLocation)
    {
        _logger.LogInformation("Grabbing " + mediaLocation.ListName + " files from directory " + mediaLocation.Directory);
        if ((mediaLocation.ListName == ListType.Compressed) && (!mediaLocation.Directory.IsNullOrEmpty()))
        {
            await ReadCompressedFilesFromDirectory(mediaLocation);
        }
        else if ((mediaLocation.ListName == ListType.Uncompressed) && (!mediaLocation.Directory.IsNullOrEmpty()))
        {
            await ReadMediaFilesFromDirectory(mediaLocation, 2);
        }
        else if ((mediaLocation.ListName == ListType.Current)  && (!mediaLocation.Directory.IsNullOrEmpty()))
        {
            await ReadMediaFilesFromDirectory(mediaLocation, 3);
        }
    }

    private async Task ReadMediaFilesFromDirectory(MediaLocationDto mediaLocation, int sequence)
    {
        try
        {
            // Check if directory exists
            if (Directory.Exists(mediaLocation.Directory))
            {
                foreach (var dir in Directory.GetDirectories(mediaLocation.Directory))
                {
                    foreach (var mediaType in mediaTypes)
                    {
                        foreach (var file in Directory.GetFiles(dir, "*." + mediaType))
                        {
                            try
                            {
                                FileInfo fi = new FileInfo(file);
                                var rec = new CreateFileEntryDto
                                {
                                    Server = _currentLocation,
                                    Directory = dir,
                                    Filename = Path.GetFileNameWithoutExtension(fi.Name),
                                    ListName = mediaLocation.ListName,
                                    Extn = Path.GetExtension(fi.Name),
                                    Size = fi.Length,
                                    Sequence = sequence
                                };
                                await _fileEntryService.CreateFileEntryAsync(rec);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogDebug(ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                _logger.LogDebug("Directory "  + mediaLocation.Directory + " does not exist on " + _currentLocation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
        }
        _logger.LogDebug("End ReadMediaFilesFromLocalDirectory");
    }
    
    private async Task ReadCompressedFilesFromDirectory(MediaLocationDto mediaLocation)
    {
        _logger.LogInformation("Read Compressed Files from: " + 
                               _currentLocation + " and folder:" + mediaLocation.Directory);
        try
        {
            // Check if directory exists
            if (Directory.Exists(mediaLocation.Directory))
            {
                string folder = mediaLocation.Directory + "//";

                var dirs = Directory.GetDirectories(folder);
                foreach (string dir in dirs)
                {
                    foreach (var type in compressType)
                    {
                        string pattern = "*." + type;
                        var files = Directory.GetFiles(dir, pattern);
                        foreach (var file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            try
                            {
                                var rec = new CreateFileEntryDto
                                {
                                    Server = _currentLocation,
                                    Directory = fi.Directory.ToString(),
                                    Filename = Path.GetFileNameWithoutExtension(fi.Name),
                                    ListName = mediaLocation.ListName,
                                    Extn = Path.GetExtension(fi.Name),
                                    Size = fi.Length,
                                    Sequence = 0
                                };
                                await _fileEntryService.CreateFileEntryAsync(rec);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogDebug(ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                _logger.LogDebug("Directory "  + mediaLocation.Directory + " does not exist on " + _currentLocation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("ReadCompressedFilesFromLocalDirectory Error:" + ex.Message);
        }
        _logger.LogDebug("End ReadCompressedFilesFromLocalDirectory");
    }
}
