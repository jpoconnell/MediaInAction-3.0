using System.Collections.Generic;

namespace MediaInAction.EmbyService.Config;

public sealed class EmbySettings 
{
    public int Id { get; set; }
    public bool Enable { get; set; }
    public List<EmbyServers> Servers { get; set; } = new List<EmbyServers>();
}

public class EmbyServers
{
    public string ServerId { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string AdministratorId { get; set; }
    public string ServerHostname { get; set; }
    public bool EnableEpisodeSearching { get; set; }
    public List<EmbySelectedLibraries> EmbySelectedLibraries { get; set; } = new List<EmbySelectedLibraries>();
    public string Ip { get; set; }
    public string FullUri { get; set; }
}
    
public class EmbySelectedLibraries
{
    public string Key { get; set; }
    public string Title { get; set; } // Name is for display purposes
    public string CollectionType { get; set; }
    public bool Enabled { get; set; }
}
