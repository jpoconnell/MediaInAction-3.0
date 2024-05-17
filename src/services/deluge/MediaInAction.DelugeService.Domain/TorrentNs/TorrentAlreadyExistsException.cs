using Volo.Abp;

namespace MediaInAction.DelugeService.TorrentNs
{
    public class TorrentAlreadyExistsException : BusinessException
    {
        public TorrentAlreadyExistsException(string name)
            : base(DelugeServiceErrorCodes.TorrentAlreadyExists)
        {
            WithData("name", name);
        }
    }
}