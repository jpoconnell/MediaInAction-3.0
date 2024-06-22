using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class NoDateSpecification : Specification<Episode>
{
    public NoDateSpecification()
    {
    }
    
    public override Expression<Func<Episode, bool>> ToExpression()
    {
        return query => query.AiredDate < DateTime.Now.AddYears(-5);
    }
}