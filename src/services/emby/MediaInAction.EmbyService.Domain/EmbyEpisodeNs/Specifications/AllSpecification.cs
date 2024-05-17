using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs.Specifications;

public class AllSpecification : Specification<EmbyEpisode>
{
    
    public AllSpecification()
    {
    }

    public override Expression<Func<EmbyEpisode, bool>> ToExpression()
    {
        return query => query.SeasonNum >= 0;
    }
}