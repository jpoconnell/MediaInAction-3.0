using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.VideoService.EpisodeAliasNs
{
    public class EpisodeAliasDto
    {
        public Guid EpisodeId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}

