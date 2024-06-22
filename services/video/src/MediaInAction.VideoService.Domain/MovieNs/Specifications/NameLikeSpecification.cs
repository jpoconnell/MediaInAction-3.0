using System;
using System.Linq.Expressions;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs.Specifications;

public class NameLikeSpecification : Specification<Movie>
{
    protected string PartialName { get; set; }

    public NameLikeSpecification(string name)
    {
        PartialName = name;
    }

    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return query => query.Name.Contains(PartialName);
    }
}