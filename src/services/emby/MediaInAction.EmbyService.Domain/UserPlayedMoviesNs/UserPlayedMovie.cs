using System;
using Volo.Abp.Domain.Entities;

namespace  MediaInAction.EmbyService.UserPlayedMoviesNs;

public class UserPlayedMovie : IEntity<Guid>
{
    public object[] GetKeys()
    {
        throw new NotImplementedException();
    }

    public Guid Id { get; }
    public int TheMovieDbId { get; set; }
    public Guid UserId { get; set; }
}
