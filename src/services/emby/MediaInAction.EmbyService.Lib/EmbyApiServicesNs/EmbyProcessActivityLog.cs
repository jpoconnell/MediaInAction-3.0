using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EmbyClient.Dotnet.Api;
using EmbyClient.Dotnet.Client;
using EmbyClient.Dotnet.Model;
using MediaInAction.EmbyService.ActivityLogEntryNs;
using Microsoft.Extensions.Logging;
using ActivityLogEntry = EmbyClient.Dotnet.Model.ActivityLogEntry;

namespace MediaInAction.EmbyService.EmbyApiServicesNs;

public class EmbyProcessActivityLog :  IEmbyProcessActivityLog
{
    private readonly EmbyActivityLogEntryManager _activityLogEntryManager;
    private readonly IEmbyActivityLogEntryRepository _activityLogEntryRepository;
    private readonly ILogger<EmbyProcessActivityLog> _logger;
    
    public EmbyProcessActivityLog(
        ILogger<EmbyProcessActivityLog> logger,
        EmbyActivityLogEntryManager activityLogManager,
        IEmbyActivityLogEntryRepository activityLogEntryRepository
        ) 
    {
        _logger = logger;
        _activityLogEntryManager = activityLogManager;
        _activityLogEntryRepository = activityLogEntryRepository;
    }

    public async Task UpdateActivities()
    {
        var activityLogList = GetData();
        await UpdateDatabase(activityLogList);
    }
    
    public List<ActivityLogEntry> GetData()
    {
        _logger.LogInformation("Activity Log GetData Started");
        try
        {
            SetConfiguration();
        }
        catch 
        {}

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

    private async Task UpdateDatabase(List<ActivityLogEntry> activityList)
    {
        var activityLogList = await _activityLogEntryRepository.GetListAsync();
        
        foreach (var activityEntry in activityList)
        {
            var found = false;
            foreach (var activityLog in activityLogList)
            {
                if (activityLog.Date == activityEntry.Date)
                {
                    found = true;
                    break;
                }
            }

            if (found == false)
            {
                if (activityEntry.Id != null)
                {
                    var externalId = (long)activityEntry.Id;
                    if (activityEntry.Date != null)
                    {
                        var myDate = (DateTimeOffset) activityEntry.Date;
                        var mySeverity = (int)activityEntry.Severity;
                        var dbActivityLog = await _activityLogEntryManager.CreateAsync(externalId, 
                            activityEntry.Name, activityEntry.Overview, activityEntry.ShortOverview, 
                            activityEntry.Type, activityEntry.ItemId, myDate,
                            activityEntry.UserId, activityEntry.UserPrimaryImageTag, 0);
                    }
                }
            }
        }
    }

    private void SetConfiguration()
    {        
        if (Configuration.Default.BasePath != "http://feederbox7.jpocl.com:8096/emby")
        {
            Configuration.Default.Username = "jerryocl";
            Configuration.Default.ApiKey.Add("api_key", "8ea083c3fdf54a94aecfdac2f4e4742b");
            Configuration.Default.BasePath = "http://feederbox7.jpocl.com:8096/emby";
            Configuration.Default.Password = "spJPO1402";
        }
    }
}

