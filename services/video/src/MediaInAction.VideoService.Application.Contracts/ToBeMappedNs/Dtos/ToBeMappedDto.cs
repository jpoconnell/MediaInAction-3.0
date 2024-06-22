using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.ToBeMappedNs.Dtos;

public class ToBeMappedDto : EntityDto<Guid>
{
    public string Alias { get; set; }
    public bool Processed { get; set; }
    public int Tries { get; set; }
}