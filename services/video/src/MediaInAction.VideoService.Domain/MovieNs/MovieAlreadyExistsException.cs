using Volo.Abp;

namespace MediaInAction.VideoService.MovieNs;

public class MovieAlreadyExistsException : BusinessException
{
    public MovieAlreadyExistsException(string name)
        : base(VideoServiceDomainErrorCodes.MovieAlreadyExists)
    {
        WithData("name", name);
    }
}
