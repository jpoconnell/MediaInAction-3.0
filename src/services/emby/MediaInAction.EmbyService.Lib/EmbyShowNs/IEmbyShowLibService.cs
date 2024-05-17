using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaInAction.EmbyService.EmbyShowNs;

public interface IEmbyShowLibService
{
    Task UpdateAddFromDto(EmbyShowDto show);
    Task<List<EmbyShowDto>> GetAll();
    Task SendAllShowsEventList();
}
