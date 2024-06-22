using System;
using System.Linq.Expressions;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs.Specifications;

public class StatusSpecification : Specification<TraktShow>
{
    protected int ShowStatus { get; set; }

    public StatusSpecification(int status)
    {
        ShowStatus = status;
    }

    public override Expression<Func<TraktShow, bool>> ToExpression()
    {
        return query => query.TraktStatus == (FileStatus) ShowStatus;
    }
}