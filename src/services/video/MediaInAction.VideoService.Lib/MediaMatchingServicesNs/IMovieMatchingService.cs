using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs.Dtos;

namespace MediaInAction.VideoService.MediaMatchingServicesNs
{
    public interface IMovieMatchingService
    {
        Task<bool> GetByMovieName(string alias, List<MovieAliasDto> list);
    }
}