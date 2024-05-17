using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<Torrent> Create(string filter)
    {
        if (filter.IsNullOrEmpty())
        {
            return new Last30DaysSpecification();
        }
        
        if (filter.StartsWith("st:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new StatusSpecification((FileStatus) status);
        }
        
        return new  Last30DaysSpecification();
    }
}