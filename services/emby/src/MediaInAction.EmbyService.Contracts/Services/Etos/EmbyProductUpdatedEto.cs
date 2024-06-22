using System;

namespace MediaInAction.EmbyService.Services;

public class EmbyProductUpdatedEto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}