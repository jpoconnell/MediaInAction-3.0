using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs.Specifications;

public class YearSpecification : Specification<FileEntry>
{
    protected int Year { get; set; }

    public YearSpecification(int year)
    {
        Year = year;
    }

    public override Expression<Func<FileEntry, bool>> ToExpression()
    {
        return query => query.CreationTime.Year == Year;
    }
}