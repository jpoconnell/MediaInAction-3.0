using System;
using System.Linq.Expressions;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class StatusSpecification : Specification<TraktEpisode>
{
    protected int EpisodeStatus { get; set; }

    public StatusSpecification(int status)
    {
        EpisodeStatus = status;
    }

    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        return query => query.TraktStatus == (FileStatus) EpisodeStatus;
    }
}