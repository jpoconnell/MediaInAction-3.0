using MediaInAction.VideoService;
using Volo.Abp;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeDoesNotExistException : BusinessException
{
    public EpisodeDoesNotExistException(string name)
        : base(VideoServiceErrorCodes.EpisodeDoesNotExistException)
    {
        WithData("name", name);
    }
}
