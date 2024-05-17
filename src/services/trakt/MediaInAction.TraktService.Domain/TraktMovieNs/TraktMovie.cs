using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMovie : AuditedAggregateRoot<Guid>
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<( string idType, string idValue)> TraktMovieAliases { get; set; }
    public bool IsActive { get; set; }
    public FileStatus TraktStatus { get; set; }
    private TraktMovie()
    {
    }

    internal TraktMovie(
        Guid id,
        [NotNull] string name, 
        int year,
        List<( string idType, string idValue)> traktMovieAliases,
        string slug ="" )
    {
        Id = id;
        SetName(Check.NotNullOrWhiteSpace(name, nameof(name)));
        Slug = slug;
        TraktMovieAliases = traktMovieAliases;
        SetYear(year);
        IsActive = true;
    }

    public TraktMovie SetYear(int year)
    {
        if (year < 1940)
        {
            throw new ArgumentException($"{nameof(year)} can not be less than 1940!");
        }

        FirstAiredYear = year;
        return this;
    }
    
    public TraktMovie SetName([NotNull] string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        Name = name;
        return this;
    }
}