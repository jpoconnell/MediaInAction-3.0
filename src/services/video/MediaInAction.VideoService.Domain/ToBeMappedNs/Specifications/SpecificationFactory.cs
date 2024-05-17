using System;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.ToBeMappedNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<ToBeMapped> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Last30DaysSpecification();
        }
        
        if (filter.StartsWith("st:"))
        {
            var status = bool.Parse(filter.Split(':')[1]);
            return new StatusSpecification(status);
        }
        
        return new Last30DaysSpecification();
    }
}