using System.Threading.Tasks;

namespace MediaInAction.VideoService.DataMaintenanceNs;

public interface IDataMaintenance
{
    Task Process();
}