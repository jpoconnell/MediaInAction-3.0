using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class AliasValueSpecification : Specification<TraktEpisode>
{
    protected string AliasValue { get; set; }

    public AliasValueSpecification(string status)
    {
        AliasValue = status;
    }

    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        return null;
    }
}