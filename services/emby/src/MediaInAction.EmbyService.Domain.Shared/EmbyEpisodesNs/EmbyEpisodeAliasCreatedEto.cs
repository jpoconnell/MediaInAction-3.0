﻿using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;

public class EmbyEpisodeAliasCreatedEto : EtoBase
{
    public string IdType { get; set; }
    public string IdValue  { get; set; }
    
}