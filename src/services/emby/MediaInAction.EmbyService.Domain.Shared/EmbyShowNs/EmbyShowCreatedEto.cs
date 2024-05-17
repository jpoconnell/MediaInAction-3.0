using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyShowNs;

[EventName("MediaInAction.EmbyShow.Created")]
public class EmbyShowCreatedEto : EtoBase
{
    public string Id { get; set; }
    public string Server { get; set; }
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
}