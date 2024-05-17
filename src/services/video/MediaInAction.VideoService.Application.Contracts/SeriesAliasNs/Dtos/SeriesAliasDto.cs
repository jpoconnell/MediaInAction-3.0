using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesAliasNs.Dtos;

    public class SeriesAliasDto : EntityDto<Guid>
    {
        public Guid SeriesId{ get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
