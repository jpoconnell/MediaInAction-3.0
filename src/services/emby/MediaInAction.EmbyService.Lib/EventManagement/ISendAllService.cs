using System.Threading.Tasks;

namespace MediaInAction.EmbyService.EventManagement
{
    public interface IEmbySendAllService
    {
        Task SendAllShows();
        Task SendAllMovies();
    }
}