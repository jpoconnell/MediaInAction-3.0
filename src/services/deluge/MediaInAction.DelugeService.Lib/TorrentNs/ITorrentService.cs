using System.Threading.Tasks;

namespace MediaInAction.DelugeService.TorrentNs;

public interface ITorrentService
{
    Task GetTorrentCollection();
}

