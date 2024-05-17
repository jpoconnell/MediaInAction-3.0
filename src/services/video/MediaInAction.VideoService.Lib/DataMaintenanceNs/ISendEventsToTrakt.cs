using System.Threading.Tasks;

namespace MediaInAction.VideoService.DataMaintenanceNs;

public interface ISendEventsToTrakt
{
    Task Process();
}