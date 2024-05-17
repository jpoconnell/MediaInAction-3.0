using System.Threading.Tasks;

namespace MediaInAction.TraktService.TraktShowSeasonNs;
public interface ITraktShowSeasonService
{
    Task DoEpisodeCleanup();
}
