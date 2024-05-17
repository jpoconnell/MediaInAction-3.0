using Volo.Abp;

namespace MediaInAction.VideoService.MovieNs;

public class MovieAlreadyExistsException : BusinessException
{
    public MovieAlreadyExistsException(string name)
        : base(VideoServiceErrorCodes.MovieAlreadyExists)
    {
        WithData("name", name);
    }
}
