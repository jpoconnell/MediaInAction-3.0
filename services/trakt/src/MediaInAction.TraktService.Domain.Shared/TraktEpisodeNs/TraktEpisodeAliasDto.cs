using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeAliasDto :  EntityDto<Guid>
{
    public Guid EpisodeId  { get;  set; }
    public string IdType { get;  set; }
    public string IdValue { get;  set; }

    public TraktEpisodeAliasDto ()
    {

    }
}

