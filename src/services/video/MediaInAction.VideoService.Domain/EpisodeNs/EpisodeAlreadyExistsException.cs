using MediaInAction.VideoService;
using Volo.Abp;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeAlreadyExistsException : BusinessException
{
    public EpisodeAlreadyExistsException(string name)
        : base(VideoServiceErrorCodes.EpisodeAlreadyExists)
    {
        WithData("name", name);
    }
}
