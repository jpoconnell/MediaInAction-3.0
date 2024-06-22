using System;

namespace MediaInAction.EmbyService.Services
{
    public interface IHasAnonymousId
    {
        Guid? AnonymousId { get; }
    }
}
