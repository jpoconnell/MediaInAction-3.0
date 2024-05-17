using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs.Specifications;

public class ActiveSpecification : Specification<Movie>
{
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return query => query.IsActive == true
            ;
        // && query.OrderDate <= DateTime.UtcNow;
    }
}