using System;

namespace MediaInAction.BasketService.Services
{
    public interface IHasAnonymousId
    {
        Guid? AnonymousId { get; }
    }
}
