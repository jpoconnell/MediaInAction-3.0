using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.TraktService.TraktMovieNs;


[EventName("MediaInAction.TraktService.TraktMovie.Accepted")]
public class TraktMovieAcceptedEto : EtoBase
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name  { get; set; }
    public int Year { get; set; }
  
}