using System;
using System.Linq.Expressions;
using MediaInAction.VideoService.EpisodeNs;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class NoDateSpecification : Specification<TraktEpisode>
{
    public NoDateSpecification()
    {
    }
    
    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        return query => query.AiredDate < DateTime.Now.AddYears(-5);
    }
}