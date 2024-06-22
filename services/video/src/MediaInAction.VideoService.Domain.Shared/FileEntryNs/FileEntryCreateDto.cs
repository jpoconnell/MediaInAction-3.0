

using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntryCreateDto 
{
    public string ExternalId { get; set; }
    public string Server { get; set; }
    public string Directory { get; set; }
    public string FileName { get; set; }
    public string Resolution { get; set; }
    public string Extn { get; set; }
    public long Size { get; set; }
    public ListType ListName { get; set; }
    public int Sequence { get; set; }
    public FileStatus FileStatus { get; set; }
    public string ErrorMessage { get; set; }
}

