using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs.Specifications;

public class MonthsAgoSpecification : Specification<FileEntry>
{
    protected int NumberOfMonths { get; set; }

    public MonthsAgoSpecification(int months)
    {
        NumberOfMonths = months;
    }

    public override Expression<Func<FileEntry, bool>> ToExpression()
    {
        var monthsAgo = DateTime.UtcNow.AddMonths(-NumberOfMonths);
        return query => query.CreationTime >= monthsAgo;
    }
}