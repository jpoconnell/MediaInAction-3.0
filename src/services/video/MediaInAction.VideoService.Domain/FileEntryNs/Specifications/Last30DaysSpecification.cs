using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.FileEntryNs.Specifications;

public class Last30DaysSpecification : Specification<FileEntry>
{
    public override Expression<Func<FileEntry, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
        // && query.FileEntryDate <= DateTime.UtcNow;
    }
}