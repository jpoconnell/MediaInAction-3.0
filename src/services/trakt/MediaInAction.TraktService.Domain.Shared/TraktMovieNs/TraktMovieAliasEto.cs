using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieAliasEto : EtoBase
    {
        public string MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}