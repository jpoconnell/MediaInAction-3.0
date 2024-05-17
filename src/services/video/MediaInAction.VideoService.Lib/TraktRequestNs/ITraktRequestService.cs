using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.DataMaintenanceNs.Dtos;

namespace  MediaInAction.VideoService.TraktRequestNs;

public interface ITraktRequestService
{
    Task SendRequest(List<SeriesSeasonDto> showSeasonList);
}