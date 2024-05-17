using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.TorrentNs.Dtos;

public class TorrentDto : EntityDto<Guid>
{
    public string Comment { get; set; }
    public bool IsSeed { get; set; }
    public string Hash { get; set; }
    public bool Paused { get; set; }
    public double Ratio { get; set; }
    public string Message { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public long Added { get; set; }
    public double CompleteTime { get; set; }
    public string DownloadLocation { get; set; }
        
    public FileStatus TorrentStatus { get; set; }
    public MediaType Type { get; set; }
    public Guid MediaLink { get; set; }
    public Guid EpisodeLink { get; set; }
    public int Updates { get; set; }
    public bool IsMapped { get; set; }
   
}