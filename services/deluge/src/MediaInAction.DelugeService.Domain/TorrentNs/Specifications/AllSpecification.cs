using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace  MediaInAction.DelugeService.TorrentNs.Specifications;

public class AllSpecification : Specification<Torrent>
{
    public override Expression<Func<Torrent, bool>> ToExpression()
    {
        return query => query.Name.Length > 0;
    }
}