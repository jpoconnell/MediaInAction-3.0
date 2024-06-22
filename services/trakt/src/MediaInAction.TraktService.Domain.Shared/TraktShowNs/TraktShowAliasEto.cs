using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktShowNs
{
    public class TraktShowAliasEto : EtoBase
    {
        public string ShowId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}