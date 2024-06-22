using System;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<Movie> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new AllSpecification();
        }

        if (filter.StartsWith("n:"))
        {
            var name = filter.Split(':')[1];
            return new NameLikeSpecification(name);
        }
        
        if (filter.StartsWith("y:"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }

        if (filter.StartsWith("a:"))
        {
            return new ActiveSpecification();
        }
        
        return new AllSpecification();
    }
}