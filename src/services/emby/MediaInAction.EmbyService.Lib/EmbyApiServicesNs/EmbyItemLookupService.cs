using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using EmbyClient.Dotnet.Api;
using EmbyClient.Dotnet.Client;
using MediaInAction.EmbyService.EmbyItems;
using Microsoft.Extensions.Logging;

namespace MediaInAction.EmbyService.EmbyApiServicesNs;

public class EmbyItemLookupService :  IEmbyItemLookupService
{
    private readonly EmbyItemManager  _embyItemManager;
    private readonly ILogger<EmbyItemLookupService> _logger;
    
    public EmbyItemLookupService(
        ILogger<EmbyItemLookupService> logger,
        EmbyItemManager embyItemManager) 
    {
        _logger = logger;
        _embyItemManager = embyItemManager;
    }

    public async Task UpdateActivities()
    {
        var embyItemMasterList = new List<EmbyItem>();
        var embyItemSubMasterList = new List<EmbyItem>();
        var embyItemSubMasterList2 = new List<EmbyItem>();
        var embyItemList = await GetData(1, true, null);
        foreach (var embyItem in embyItemList)
        {
            Console.WriteLine(embyItem.EmbyItemLevel.ToString() +":"+ embyItem.Id);
            embyItemMasterList.Add(embyItem);
        }

        foreach (var embyItem in embyItemMasterList)
        {
            if (embyItem.EmbyItemLevel == 1)
            {
                var embyItemList2 = await GetData(2, false, embyItem);
                foreach (var embyItem2 in embyItemList2)
                {
                    Console.WriteLine(embyItem2.EmbyItemLevel.ToString() +":"+ embyItem2.Id);
                    embyItemSubMasterList.Add(embyItem2);
                }
            }
        }

        foreach (var emitem3 in embyItemSubMasterList)
        {
            embyItemMasterList.Add(emitem3);
        }
       
        foreach (var embyItem in embyItemMasterList)
        {
            if (embyItem.EmbyItemLevel == 2)
            {
                var embyItemList3 = await GetData(3, false, embyItem);
                foreach (var embyItem3 in embyItemList3)
                {
                    embyItemSubMasterList2.Add(embyItem3);
                }
            }
        }
        
        foreach (var emitem3 in embyItemSubMasterList2)
        {
            embyItemMasterList.Add(emitem3);
        }
        
        await UpdataDatabase(embyItemMasterList);
    }
    
    private async Task<List<EmbyItem>> GetData(int level, bool config, EmbyItem parent)
    {
        _logger.LogInformation("Activity Log GetData Started");
        if (config == true)
        {
            SetConfiguration();
        }
  

        var apiInstance = new ItemsServiceApi();

        // defaults
        var artistType = "";
        var maxOfficialRating = "";
        bool? hasThemeSong = null;
        bool? hasThemeVideo = null; 
        bool? hasSubtitles  = null; 
        bool? hasSpecialFeature  = null; 
        bool? hasTrailer  = null; 
        var adjacentTo = ""; 
        int? minIndexNumber  = null; 
        int? minPlayers  = null; 
        int? maxPlayers  = null; 
        int? parentIndexNumber  = null; 
        bool? hasParentalRating  = null; 
        bool? isHD  = null; 
        var locationTypes  = ""; 
        var excludeLocationTypes  = ""; 
        bool? isMissing  = null;
        bool? isUnaired  = null; 
        double? minCommunityRating  = null;
        double? minCriticRating  = null; 
        int? airedDuringSeason  = null;
        var minPremiereDate  = ""; 
        var minDateLastSaved  = "";
        var minDateLastSavedForUser  = ""; 
        var maxPremiereDate  = "";
        bool? hasOverview  = null; 
        bool? hasImdbId  = null; 
        bool? hasTmdbId  = null;
        bool? hasTvdbId  = null; 
        var excludeItemIds  = "";
        int? startIndex  = null; 
        int? limit  = null;
        bool? recursive  = null; 
        var searchTerm  = "";
        var sortOrder  = ""; 
        var parentId  = ""; 
        var fields  = ""; 
        var excludeItemTypes  = ""; 
        var includeItemTypes  = ""; 
        var anyProviderIdEquals  = ""; 
        var filters  = "";
        bool? isFavorite  = null; 
        bool? isMovie  = null;
        bool? isSeries  = null; 
        bool? isNews  = null;
        bool? isKids  = null; 
        bool? isSports  = null; 
        var mediaTypes  = ""; 
        var imageTypes  = ""; 
        var sortBy  = ""; 
        bool? isPlayed  = null;
        var genres  = ""; 
        var officialRatings  = "";
        var tags  = "";
        var years  = "";
        bool? enableImages  = null;
        bool? enableUserData  = null;
        int? imageTypeLimit  = null;
        var enableImageTypes  = "";
        var person  = "";
        var personIds  = "";
        var personTypes  = "";
        var studios  = ""; 
        var studioIds  = ""; 
        var artists  = "";
        var artistIds  = ""; 
        var albums  = ""; 
        var ids  = "";
        var videoTypes  = "";
        var containers  = ""; 
        var audioCodecs  = ""; 
        var videoCodecs  = "";
        var subtitleCodecs  = "";
        var path  = "";
        var userId  = "";
        var minOfficialRating  = "";
        bool? isLocked  = null;
        bool? isPlaceHolder  = null;
        bool? hasOfficialRating  = null;
        bool? groupItemsIntoCollections  = null; 
        bool? is3D  = null; 
        var seriesStatus  = "";
        var nameStartsWithOrGreater  = "";
        var artistStartsWithOrGreater  = ""; 
        var albumArtistStartsWithOrGreater  = "";
        var nameStartsWith  = "";
        var nameLessThan = "";
// end defaults

         startIndex = 0;  // int? | Optional. The record index to start at. All items with a lower index will be dropped from the results. (optional) 
         limit = 200;  // int? | Optional. The maximum number of records to return (optional) 
         if (level > 1)
         {
             parentId = parent.EmbyItemId;
         }
        
        var embyItemList = new List<EmbyItem>();
        try
        {
            // Gets activity log entries
            var result = await apiInstance.GetItemsAsync(artistType,
                maxOfficialRating, hasThemeSong, hasThemeVideo, hasSubtitles, hasSpecialFeature,
                hasTrailer, adjacentTo, minIndexNumber, minPlayers, maxPlayers, parentIndexNumber,
                hasParentalRating, isHD, locationTypes, excludeLocationTypes, isMissing,
                isUnaired, minCommunityRating, minCriticRating, airedDuringSeason, minPremiereDate,
                minDateLastSaved, minDateLastSavedForUser, maxPremiereDate, hasOverview, hasImdbId,
                hasTmdbId, hasTvdbId, excludeItemIds, startIndex, limit, recursive, searchTerm,
                sortOrder, parentId, fields, excludeItemTypes, includeItemTypes, anyProviderIdEquals,
                filters, isFavorite, isMovie, isSeries, isNews, isKids, isSports, mediaTypes,
                imageTypes, sortBy, isPlayed, genres, officialRatings, tags, years, enableImages,
                enableUserData, imageTypeLimit, enableImageTypes, person, personIds, personTypes,
                studios, studioIds, artists, artistIds, albums, ids, videoTypes, containers,
                audioCodecs, videoCodecs, subtitleCodecs, path, userId, minOfficialRating, isLocked, 
                isPlaceHolder, hasOfficialRating, groupItemsIntoCollections, is3D, seriesStatus, 
                nameStartsWithOrGreater, artistStartsWithOrGreater, albumArtistStartsWithOrGreater, 
                nameStartsWith, nameLessThan);
            foreach (var line in result.Items)
            {
                var newItem = new EmbyItem();
                newItem.EmbyItemId = line.Id;
                newItem.ServerId = line.ServerId;
                newItem.Type = line.Type;
                newItem.EmbyItemLevel = level;
                if (line.MediaType != null)
                {
                    newItem.MediaType = line.MediaType;
                }
                if (line.Name != null)
                {
                    newItem.Name = line.Name;
                }
                if (line.RunTimeTicks != null)
                {
                    newItem.RunTimeTicks = line.RunTimeTicks;
                }
                if (line.ParentId != null)
                {
                    newItem.ParentId = line.ParentId;
                }
                embyItemList.Add(newItem);
            }
            return embyItemList;
        }
        catch (Exception e)
        {
            Debug.Print("Exception when calling ActivityLogServiceApi.GetSystemActivitylogEntries: " + e.Message );
            return null;
        }
    }

    private async Task UpdataDatabase(List<EmbyItem> embyItemList)
    {
        foreach (var embyItem in embyItemList)
        {
            var newEmbyItem = await _embyItemManager.CreateAsync(embyItem.EmbyItemId, 
                embyItem.ServerId, embyItem.Type,embyItem.EmbyItemLevel, 
                embyItem.ParentId, embyItem.RunTimeTicks, embyItem.MediaType);
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

