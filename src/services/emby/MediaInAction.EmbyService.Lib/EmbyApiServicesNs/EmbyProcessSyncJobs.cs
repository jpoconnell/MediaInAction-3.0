using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EmbyClient.Dotnet.Api;
using EmbyClient.Dotnet.Client;
using EmbyClient.Dotnet.Model;
using MediaInAction.EmbyService.ActivityLogEntryNs;
using MediaInAction.EmbyService.EmbyActivityLogEntryNs;
using Microsoft.Extensions.Logging;
using ActivityLogEntry = EmbyClient.Dotnet.Model.ActivityLogEntry;

namespace MediaInAction.EmbyService.EmbyApiServicesNs;

public class EmbyProcessSyncJobs :  IEmbyProcessSyncJobs
{
    private readonly EmbyActivityLogEntryManager _activityLogManager;
    private readonly ILogger<EmbyProcessActivityLog> _logger;
    
    public EmbyProcessSyncJobs(
        ILogger<EmbyProcessActivityLog> logger,
        EmbyActivityLogEntryManager activityLogManager) 
    {
        _logger = logger;
        _activityLogManager = activityLogManager;
    }

    public async Task UpdateActivities()
    {
        var activityLogList = GetData();
        await UpdataDatabase(activityLogList);
    }
    
    public List<ActivityLogEntry> GetData()
    {
        _logger.LogInformation("Activity Log GetData Started");
        SetConfiguration();

        var apiInstance = new ActivityLogServiceApi();
        var startIndex = 0;  // int? | Optional. The record index to start at. All items with a lower index will be dropped from the results. (optional) 
        var limit = 200;  // int? | Optional. The maximum number of records to return (optional) 
        var minDate = "2000-01-01";  // string | Optional. The minimum date. Format = ISO (optional) 

        var activityLogList = new List<ActivityLogEntry>();
        try
        {
            // Gets activity log entries
            QueryResultActivityLogEntry result = apiInstance.GetSystemActivitylogEntries(startIndex, limit, minDate);
            foreach (var line in result.Items)
            {
                activityLogList.Add(line);
            }
            return activityLogList;
        }
        catch (Exception e)
        {
            Debug.Print("Exception when calling ActivityLogServiceApi.GetSystemActivitylogEntries: " + e.Message );
            return null;
        }
    }

    private async Task UpdataDatabase(List<ActivityLogEntry> activityList)
    {
        foreach (var activityEntry in activityList)
        {
            if (activityEntry.Id != null)
            {
                var externalId = (long)activityEntry.Id;
                if (activityEntry.Date != null)
                {
                    var myDate = (DateTimeOffset) activityEntry.Date;
                    var mySeverity = (int)activityEntry.Severity;
                    var dbActivityLog = await _activityLogManager.CreateAsync(externalId, 
                        activityEntry.Name, activityEntry.Overview, activityEntry.ShortOverview, 
                        activityEntry.Type, activityEntry.ItemId, myDate,
                        activityEntry.UserId, activityEntry.UserPrimaryImageTag, 0);
                }
            }
        }
    }

    private void SetConfiguration()
    {
        if (Configuration.Default.BasePath != "\"http://feederbox7.jpocl.com:8096/emby\";")
        {
            Configuration.Default.Username = "jerryocl";
            Configuration.Default.ApiKey.Add("api_key", "8ea083c3fdf54a94aecfdac2f4e4742b");
            Configuration.Default.BasePath = "http://feederbox7.jpocl.com:8096/emby";
            Configuration.Default.Password = "spJPO1402";
        }

    }
}

