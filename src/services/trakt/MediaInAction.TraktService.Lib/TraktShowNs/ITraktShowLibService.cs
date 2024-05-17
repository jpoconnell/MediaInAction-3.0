using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.ShowNs.Dtos;

namespace MediaInAction.TraktService.TraktShowNs;

public interface ITraktShowLibService
{
    Task UpdateAddFromDto(CollectionShowDto show);
    Task<List<TraktShowDto>> GetShows();

    Task<List<TraktShowDto>> GetActiveShows();

    Task ResendUnAcceptedShowsList();
}
