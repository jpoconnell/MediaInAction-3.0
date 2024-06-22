using System;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<Episode> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Last30DaysSpecification();
        }

        if (filter.StartsWith("y:"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }

        if (filter.StartsWith("m:"))
        {
            var months = int.Parse(filter.Split('m')[1]);
            return new MonthsAgoSpecification(months);
        }
        
        if (filter.StartsWith("s:"))
        {
            var season = int.Parse(filter.Split(':')[1]);
            return new SeasonSpecification(season);
        }
        if (filter.StartsWith("st:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new StatusSpecification(status);
        }
        if (filter.StartsWith("n:"))
        {
            var idValue = (filter.Split(':')[1]);
            return new AliasValueSpecification(idValue);
        }
        if (filter.StartsWith("nd:"))
        {
            var idValue = (filter.Split(':')[1]);
            return new NoDateSpecification();
        }

        return new Last30DaysSpecification();
    }
}