using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class MonthsAgoSpecification : Specification<Episode>
{
    protected int NumberOfMonths { get; set; }

    public MonthsAgoSpecification(int months)
    {
        NumberOfMonths = months;
    }

    public override Expression<Func<Episode, bool>> ToExpression()
    {
        var monthsAgo = DateTime.UtcNow.AddMonths(-NumberOfMonths);
        return query => query.AiredDate >= monthsAgo;
    }
}