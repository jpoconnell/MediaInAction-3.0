using System;

namespace MediaInAction.EmbyService.Services;

public class RemoveProductDto : IHasAnonymousId
{
    public Guid ProductId { get; set; }

    public int? Count { get; set; }

    public Guid? AnonymousId { get; set; }
}