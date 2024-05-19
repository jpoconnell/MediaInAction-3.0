using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class SeasonSpecification : Specification<TraktEpisode>
{
    protected int Season { get; set; }

    public SeasonSpecification(int season)
    {
        Season = season;
    }

    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        return query => query.SeasonNum == Season;
    }
}