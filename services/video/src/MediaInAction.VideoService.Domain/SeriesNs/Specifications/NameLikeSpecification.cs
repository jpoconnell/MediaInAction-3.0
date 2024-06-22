using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs.Specifications;

public class NameLikeSpecification : Specification<Series>
{
    protected string PartialName { get; set; }

    public NameLikeSpecification(string name)
    {
        PartialName = name;
    }

    public override Expression<Func<Series, bool>> ToExpression()
    {
        return query => query.Name.Contains(PartialName);
    }
}