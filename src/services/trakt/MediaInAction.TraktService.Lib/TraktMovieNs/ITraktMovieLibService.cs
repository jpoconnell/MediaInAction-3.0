using System.Threading.Tasks;
using MediaInAction.TraktService.MovieNs.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs;

public interface ITraktMovieLibService
    {
        Task UpdateAddFromDto(TraktCollectionMovieDto movie);

        Task ResendUnAcceptedMoviesList();
    }
