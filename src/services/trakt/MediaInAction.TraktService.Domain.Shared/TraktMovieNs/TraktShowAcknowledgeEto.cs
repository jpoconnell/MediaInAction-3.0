using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktMovieNs;

[EventName("MediaInAction.TraktMovie.Acknowledge")]
public class TraktMovieAcknowledgeEto : EtoBase
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name  { get; set; }
    public int Year { get; set; }
}