using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.MovieAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.MovieNs;

public class Movie : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public MediaType Type { get; set; }
    public bool IsActive { get; set; }
    public MediaStatus MovieStatus { get; set; }
    public MediaStatus MediaStatus { get; set; }
    public FileStatus EventStatus { get; set; }
    public List<MovieAlias> MovieAliases { get; set; }

    public Movie()
    {
    }

    internal Movie(
        Guid id,
        [NotNull] string name,
        int firstAiredYear,
        MediaType movieType = MediaType.Movie,
        MediaStatus status = MediaStatus.New,
        bool isActive = true
    )
        : base(id)
    {
        Name = name;
        Type = movieType;
        FirstAiredYear = firstAiredYear;
        IsActive = isActive;
        MovieStatus = status;
        MovieAliases = new List<MovieAlias>();
    }
    
    public Movie AddMovieAlias(Guid id, Guid movieId, string idType, string idValue )
    {
        if (MovieAliases == null)
        {
            MovieAliases = new List<MovieAlias>();
            var movieAlias = new MovieAlias(id, movieId, idType, idValue);
            MovieAliases.Add(movieAlias);
        }
        else
        {
            var existingAliasForMovie = MovieAliases.SingleOrDefault(o => o.MovieId == movieId &&
                                                                          o.IdType == idType && 
                                                                          o.IdValue == idValue);

            if (existingAliasForMovie != null)
            {

            }
            else
            {
                var movieAlias = new MovieAlias(id, movieId, idType, idValue);
                MovieAliases.Add(movieAlias);
            }
        }
        
        return this;
    }

    public MediaStatus SetMovieStatus(MediaStatus status)
    {
        MovieStatus = status;
        return MovieStatus;
    }
}