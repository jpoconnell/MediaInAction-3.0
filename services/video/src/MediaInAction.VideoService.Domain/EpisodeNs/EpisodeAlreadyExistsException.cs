using MediaInAction.VideoService;
using Volo.Abp;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeAlreadyExistsException : BusinessException
{
    public EpisodeAlreadyExistsException(string name)
        : base(VideoServiceDomainErrorCodes.EpisodeAlreadyExists)
    {
        WithData("name", name);
    }
}
