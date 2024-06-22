using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class SeasonSpecification : Specification<Episode>
{
    protected int Season { get; set; }

    public SeasonSpecification(int season)
    {
        Season = season;
    }

    public override Expression<Func<Episode, bool>> ToExpression()
    {
        return query => query.SeasonNum == Season;
    }
}