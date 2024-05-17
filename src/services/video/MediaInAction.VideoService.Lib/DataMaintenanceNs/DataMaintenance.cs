using System.Threading.Tasks;

namespace  MediaInAction.VideoService.DataMaintenanceNs;

public class DataMaintenance : IDataMaintenance
{
    private readonly IProcessMappedFiles _processMappedFiles;
    private readonly IProcessToBeMappeds _processToBeMapped;
    private readonly ISendEventsToTrakt _sendEventsToTrakt;
    public DataMaintenance( 
        IProcessMappedFiles processMappedFiles,
        IProcessToBeMappeds processToBeMapped,
        ISendEventsToTrakt sendEventsToTrakt
      )
    {
        _processMappedFiles = processMappedFiles;
        _processToBeMapped = processToBeMapped;
        _sendEventsToTrakt = sendEventsToTrakt;
    }

    public async Task Process()
    {
        await _processToBeMapped.Process();
        await _processMappedFiles.Process();
        await _sendEventsToTrakt.Process();
    }
}