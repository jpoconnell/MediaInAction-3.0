using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class Last30DaysSpecification : Specification<Episode>
{
    public override Expression<Func<Episode, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}