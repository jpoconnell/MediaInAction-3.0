using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.TorrentNs.Specifications;

public class LabelSpecification : Specification<Torrent>
{
    protected string Label { get; set; }

    public LabelSpecification(string status)
    {
        Label = status;
    }

    public override Expression<Func<Torrent, bool>> ToExpression()
    {
        return query => query.Label ==  Label;
    }
}