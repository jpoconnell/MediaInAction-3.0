using System;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowsNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<EmbyShow> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new AllSpecification();
        }

        if (filter.StartsWith("y"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }
        
        return new AllSpecification();
    }
}