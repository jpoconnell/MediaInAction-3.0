using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;

public class EmbyEpisodeAliasDto :  EntityDto<Guid>
{
    public string IdType { get;  set; }
    public string IdValue { get;  set; }

    public EmbyEpisodeAliasDto ()
    {

    }
}

