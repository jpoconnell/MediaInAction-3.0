using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.EmbyService.EmbyShowNs;

[EventName("MediaInAction.EmbyShow.Accepted")]
public class EmbyShowAcceptedEto : EtoBase
{
    public string ExternalId  { get; set; }
    public string Name  { get; set; }
  
}