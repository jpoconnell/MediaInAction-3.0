using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs.Specifications;

public class Last30DaysSpecification : Specification<ToBeMapped>
{
    public override Expression<Func<ToBeMapped, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}