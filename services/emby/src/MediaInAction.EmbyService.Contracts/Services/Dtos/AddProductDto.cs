using System;
using System.ComponentModel.DataAnnotations;

namespace MediaInAction.EmbyService.Services;

public class AddProductDto : IHasAnonymousId
{
    public Guid ProductId { get; set; }
    
    [Range(1, int.MaxValue)]
    public int Count { get; set; } = 1;

    public Guid? AnonymousId { get; set; }
}