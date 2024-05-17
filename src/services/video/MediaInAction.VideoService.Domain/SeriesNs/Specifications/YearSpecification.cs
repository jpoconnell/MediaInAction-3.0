using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs.Specifications;

public class YearSpecification : Specification<Series>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<Series, bool>> ToExpression()
    {
        return query => query.FirstAiredYear == Year;
    }
}