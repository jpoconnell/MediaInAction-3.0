using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyMovieNs;

[EventName("MediaInAction.EmbyMovie.Created")]
public class EmbyMovieCreatedEto : EtoBase
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    
}