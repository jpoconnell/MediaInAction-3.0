using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowsNs.Specifications;

public class AllSpecification : Specification<EmbyShow>
{
    
    public AllSpecification()
    {
    }

    public override Expression<Func<EmbyShow, bool>> ToExpression()
    {
        return query => query.Name.Length >= 0;
    }
}