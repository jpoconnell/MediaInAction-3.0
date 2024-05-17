using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public interface IEmbyEpisodeLibService
{
    Task UpdateAddFromDto(EmbyEpisodeDto episode);
    Task<List<EmbyEpisodeDto>> GetAll();
    Task SendAllEpisodesEventList();
}
