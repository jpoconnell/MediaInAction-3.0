using System;
using System.Linq.Expressions;
using MediaInAction.VideoService.EpisodeNs;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class MonthsAgoSpecification : Specification<TraktEpisode>
{
    protected int NumberOfMonths { get; set; }

    public MonthsAgoSpecification(int months)
    {
        NumberOfMonths = months;
    }

    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        var monthsAgo = DateTime.UtcNow.AddMonths(-NumberOfMonths);
        return query => query.AiredDate >= monthsAgo;
    }
}