using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeAliasUpdatedEto :  EtoBase
{
    public string IdType { get;  set; }
    public string IdValue { get;  set; }

    public TraktEpisodeAliasUpdatedEto ()
    {

    }
}

