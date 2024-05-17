using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.OrderingService.Orders.Specifications;

public class YearSpecification : Specification<Order>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<Order, bool>> ToExpression()
    {
        return query => query.OrderDate.Year == Year;
    }
}