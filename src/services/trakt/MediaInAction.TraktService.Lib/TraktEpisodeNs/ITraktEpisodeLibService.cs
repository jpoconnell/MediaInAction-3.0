using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.ShowNs.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public interface ITraktEpisodeLibService
{
    Task UpdateAddFromDto(CollectionEpisodeDto episode);
    Task<Guid> CreateUpdateEpisode(CollectionEpisodeDto episode);
    Task<List<TraktEpisodeDto>> GetEpisodeByShow(string slug);
    Task ResendUnAcceptedEpisodesList();
}
