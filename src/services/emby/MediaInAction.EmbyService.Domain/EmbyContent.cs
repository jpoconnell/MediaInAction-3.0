using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService;

public class EmbyContent : Entity<Guid>
{
    public string ProviderId { get; set; }
    public string EmbyId { get; set; }
    public DateTime AddedAt { get; set; }
    public string ImdbId { get; set; }
    public string TheMovieDbId { get; set; }
    public string Title { get; set; }

    public string Type { get; set; }
    public string Url { get; set; }

    public string Quality { get; set; }

    public bool Has4K { get; set; }

    public string TvDbId { get; set; }
    //public override RecentlyAddedType RecentlyAddedType => RecentlyAddedType.Emby;
}
