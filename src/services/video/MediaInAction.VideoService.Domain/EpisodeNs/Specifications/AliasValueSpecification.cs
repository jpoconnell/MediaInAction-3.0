using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class AliasValueSpecification : Specification<Episode>
{
    protected string AliasValue { get; set; }

    public AliasValueSpecification(string status)
    {
        AliasValue = status;
    }

    public override Expression<Func<Episode, bool>> ToExpression()
    {
        return null;
    }
}