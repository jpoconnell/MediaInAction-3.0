using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Settings;

namespace MediaInAction.EmbyService;

public class UserManagementSettings : ISettingStore
{
    public bool ImportPlexAdmin { get; set; }
    public bool ImportPlexUsers { get; set; }
    public bool CleanupPlexUsers { get; set; }
    public bool ImportEmbyUsers { get; set; }
    public bool ImportJellyfinUsers { get; set; }
    public Type MovieRequestLimit { get; set; }
    public RequestLimitType MovieRequestLimitType { get; set; } = RequestLimitType.Week;
    public int EpisodeRequestLimit { get; set; }
    public RequestLimitType EpisodeRequestLimitType { get; set; } = RequestLimitType.Week;
    public int MusicRequestLimit { get; set; }
    public RequestLimitType MusicRequestLimitType { get; set; } = RequestLimitType.Week;
    public string DefaultStreamingCountry { get; set; } = "US";
    public List<string> DefaultRoles { get; set; } = new List<string>();
    public List<string> BannedPlexUserIds { get; set; } = new List<string>();
    public List<string> BannedEmbyUserIds { get; set; } = new List<string>();
    public List<string> BannedJellyfinUserIds { get; set; } = new List<string>();
    public Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<SettingValue>> GetAllAsync(string[] names, string providerName, string providerKey)
    {
        throw new System.NotImplementedException();
    }
}
