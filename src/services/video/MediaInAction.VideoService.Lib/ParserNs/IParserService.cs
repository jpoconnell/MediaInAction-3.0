using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.TorrentNs.Dtos;

namespace MediaInAction.VideoService.ParserNs;

public interface IParserService
{
    Task<MediaType> GetMediaType(ParserDto input);
    Task<ParserDto> MapProcess(FileEntryDto fileEntry);
    Task<ParserDto> MapProcess(TorrentDto torrentEntry);
    Task<ParserDto> GetMoveTo(Guid link);
}