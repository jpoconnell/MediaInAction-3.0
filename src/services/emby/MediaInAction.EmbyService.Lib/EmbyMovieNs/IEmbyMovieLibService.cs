using System.Threading.Tasks;
using MediaInAction.EmbyService.Models;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public interface IEmbyMovieLibService
{
    Task UpdateAddFromDto(EmbyMovieDto movie);
    Task SendAllMoviesEventList();
}
