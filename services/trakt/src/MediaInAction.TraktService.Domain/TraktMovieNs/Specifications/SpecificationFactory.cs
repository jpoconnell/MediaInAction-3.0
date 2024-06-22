using System;
using MediaInAction.TraktService.TraktMovieNs;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<TraktMovie> Create(string filter)
    {
        
        if (filter.StartsWith("s:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new StatusSpecification(status);
        }

        return null;
    }
}