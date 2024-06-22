using MediaInAction.Shared.Domain.Enums;


namespace MediaInAction.FileService.FileEntriesNs;

public class FileEntryCreateDto 
{
    public string FileEntryId { get; set; }
    
    public string Server { get; set; }

    public string FileName { get; set; }

    public string Directory { get; set; }
    
    public string Extn { get; set; }
    
    public long Size { get; set; }
    
    public int Sequence { get; set; }
    
    public ListType ListName  { get; set; }
    
    public FileStatus FileStatus  { get; set; }
}