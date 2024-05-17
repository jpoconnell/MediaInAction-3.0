using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs.Specifications;

public class YearSpecification : Specification<Movie>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return query => query.FirstAiredYear == Year;
    }
}