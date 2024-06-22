using MediaInAction.VideoService;
using Volo.Abp;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeDoesNotExistException : BusinessException
{
    public EpisodeDoesNotExistException(string name)
        : base(VideoServiceDomainErrorCodes.EpisodeDoesNotExistException)
    {
        WithData("name", name);
    }
}
