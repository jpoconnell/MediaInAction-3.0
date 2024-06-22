using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.SeriesNs;

public class Series : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public MediaStatus MediaStatus { get; set; }
    public FileStatus EventStatus { get; set; }
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
    public List<SeriesAlias> SeriesAliases { get; private set; }

    public Series()
    {
    }

    internal Series(
        [NotNull] string name,
        int firstAiredYear,
        [NotNull] MediaType seriesType,
        bool isActive = true,
        string imageName = ""
    )
    {
        Name = name;
        Type = seriesType;
        FirstAiredYear = firstAiredYear;
        IsActive = isActive;
        SeriesAliases = new List<SeriesAlias>();
    }
    
    public Series AddSeriesAlias(Guid id, Guid seriesId, string idType, string idValue )
    {
        var existingAliasForSeries = SeriesAliases.SingleOrDefault(o => o.SeriesId == seriesId &&
            o.IdType == idType && 
            o.IdValue == idValue);

        if (existingAliasForSeries != null)
        {

        }
        else
        {
            var seriesAlias = new SeriesAlias(id, seriesId, idType, idValue);
            SeriesAliases.Add(seriesAlias);
        }

        return this;
    }
    
    public Series SetSeriesInactive()
    {
        IsActive = false;
        return this;
    }
}