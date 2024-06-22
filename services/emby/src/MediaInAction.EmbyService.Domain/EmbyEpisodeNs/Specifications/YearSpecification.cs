using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs.Specifications;

public class YearSpecification : Specification<EmbyEpisode>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<EmbyEpisode, bool>> ToExpression()
    {
        return query => query.AiredDate == DateTime.Now;
    }
}