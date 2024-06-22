using System;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<TraktEpisode> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new TraktService.TraktEpisodeNs.Specifications.Last30DaysSpecification();
        }

        if (filter.StartsWith("y:"))
        {
            var year = int.Parse(filter.Split('y')[1]);
            return new YearSpecification(year);
        }

        if (filter.StartsWith("m:"))
        {
            var months = int.Parse(filter.Split('m')[1]);
            return new TraktService.TraktEpisodeNs.Specifications.MonthsAgoSpecification(months);
        }
        
        if (filter.StartsWith("s:"))
        {
            var season = int.Parse(filter.Split(':')[1]);
            return new TraktService.TraktEpisodeNs.Specifications.SeasonSpecification(season);
        }
        if (filter.StartsWith("st:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new StatusSpecification(status);
        }
        if (filter.StartsWith("nd:"))
        {
            var idValue = (filter.Split(':')[1]);
            return new TraktService.TraktEpisodeNs.Specifications.NoDateSpecification();
        }

        return new TraktService.TraktEpisodeNs.Specifications.Last30DaysSpecification();
    }
}