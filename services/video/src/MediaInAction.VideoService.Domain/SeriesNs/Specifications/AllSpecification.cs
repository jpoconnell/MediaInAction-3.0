using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace  MediaInAction.VideoService.SeriesNs.Specifications;

public class AllSpecification : Specification<Series>
{
    public override Expression<Func<Series, bool>> ToExpression()
    {
        return query => query.Name.Length > 0;
    }
}