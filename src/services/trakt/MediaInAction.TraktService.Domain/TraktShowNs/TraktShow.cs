using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShow : AuditedAggregateRoot<Guid>
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<( string idType, string idValue)> TraktShowAliases { get; set; }
    public bool IsActive { get; set; }
    public FileStatus TraktStatus { get; set; }
    private TraktShow()
    {
    }

    internal TraktShow(
        Guid id,
        [NotNull] string name, 
        int year,
        List<( string idType, string idValue)> traktShowAliases,
        string slug ="" )
    {
        Id = id;
        SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        Slug = slug;
        TraktShowAliases = traktShowAliases;
        SetYear(year);
        IsActive = true;
    }

    public TraktShow SetYear(int year)
    {
        if (year < 1940)
        {
            throw new ArgumentException($"{nameof(year)} can not be less than 1940!");
        }

        FirstAiredYear = year;
        return this;
    }
    
    public TraktShow SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
        return this;
    }
}