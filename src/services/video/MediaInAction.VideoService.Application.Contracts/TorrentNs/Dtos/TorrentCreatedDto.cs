namespace MediaInAction.VideoService.TorrentNs.Dtos;

public class TorrentCreatedDto 
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

}