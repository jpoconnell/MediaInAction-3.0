using Volo.Abp;

namespace MediaInAction.TraktService.TraktMovieNs;

    public class TraktMovieNameAlreadyExistsException : BusinessException
    {
        public TraktMovieNameAlreadyExistsException(string name)
            : base("TraktService:000001", $"A movie with name {name} has already exists!")
        {
            WithData("name", name);
        }
    }
