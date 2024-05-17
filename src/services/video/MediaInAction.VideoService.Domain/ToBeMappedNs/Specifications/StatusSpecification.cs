using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs.Specifications;

public class StatusSpecification : Specification<ToBeMapped>
{
    protected bool Processed { get; set; }

    public StatusSpecification(bool status)
    {
        Processed = status;
    }

    public override Expression<Func<ToBeMapped, bool>> ToExpression()
    {
        return query => query.Processed == (bool) Processed;
    }
}