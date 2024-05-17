using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;

namespace  MediaInAction.VideoService.FileEntryNs;

public interface IFileEntryLibService
{
    Task<List<FileEntryDto>> GetByLink(Guid episodeId);
    Task<List<FileEntryDto>> GetMapped( bool isMapped);
    Task UpdateAsync(FileEntryDto fileEntry);
}