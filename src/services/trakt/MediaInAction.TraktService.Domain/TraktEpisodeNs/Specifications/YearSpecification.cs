using System;
using System.Linq.Expressions;
using MediaInAction.VideoService.EpisodeNs;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public class YearSpecification : Specification<TraktEpisode>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<TraktEpisode, bool>> ToExpression()
    {
        return query => query.AiredDate.Year == Year;
    }
}