using System.Collections.Generic;

namespace MediaInAction.BasketService.Services;

public class BasketDto
{
    public float TotalPrice { get; set; }
    public List<BasketItemDto> Items { get; set; }

    public BasketDto()
    {
        Items = new List<BasketItemDto>();
    }
}