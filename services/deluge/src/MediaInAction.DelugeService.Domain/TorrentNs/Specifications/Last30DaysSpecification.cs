using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.TorrentNs.Specifications;

public class Last30DaysSpecification : Specification<Torrent>
{
    public override Expression<Func<Torrent, bool>> ToExpression()
    {
        var daysAgo30 = DateTime.UtcNow.Subtract(TimeSpan.FromDays(30));
        return query => query.CreationTime >= daysAgo30
            ;
    }
}
