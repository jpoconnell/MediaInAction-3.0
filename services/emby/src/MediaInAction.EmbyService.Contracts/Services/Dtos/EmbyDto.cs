using System.Collections.Generic;

namespace MediaInAction.EmbyService.Services;

public class EmbyDto
{
    public float TotalPrice { get; set; }
    public List<EmbyItemDto> Items { get; set; }

    public EmbyDto()
    {
        Items = new List<EmbyItemDto>();
    }
}