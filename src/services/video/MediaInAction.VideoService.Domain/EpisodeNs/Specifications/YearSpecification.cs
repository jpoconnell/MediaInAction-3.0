using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public class YearSpecification : Specification<Episode>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<Episode, bool>> ToExpression()
    {
        return query => query.AiredDate.Year == Year;
    }
}