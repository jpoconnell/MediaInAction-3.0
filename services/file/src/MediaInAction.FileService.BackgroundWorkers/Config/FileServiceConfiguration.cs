using System.Collections.Generic;

namespace MediaInAction.FileService.BackgroundWorkers.Config;

public sealed class FileServiceConfiguration  
{
    public int Id { get; set; }
    public bool Enable { get; set; }
    public List<FileServers> Servers { get; set; } = new List<FileServers>();
}

public class FileServers
{
    public string ServerId { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string AdministratorId { get; set; }
    public string ServerHostname { get; set; }
    public bool EnableEpisodeSearching { get; set; }
    public List<FileSelectedLibraries> EmbySelectedLibraries { get; set; } = new List<FileSelectedLibraries>();

}
    
public class FileSelectedLibraries
{
    public string Key { get; set; }
    public string Title { get; set; } // Name is for display purposes
    public string CollectionType { get; set; }
    public bool Enabled { get; set; }
}
