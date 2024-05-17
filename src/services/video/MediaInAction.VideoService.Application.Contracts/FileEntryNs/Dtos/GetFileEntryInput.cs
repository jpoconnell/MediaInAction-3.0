using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

public class GetFileEntryInput
{
    public string Server { get; set; }
    public string Directory { get; set; }
    public string FileName { get; set; }
    public ListType ListName { get; set; }
}