using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeAliasCreatedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}