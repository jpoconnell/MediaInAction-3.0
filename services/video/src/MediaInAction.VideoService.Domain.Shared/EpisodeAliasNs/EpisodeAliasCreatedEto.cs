using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.VideoService.EpisodesAliasNs
{
    public class EpisodeAliasCreatedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}
