using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.VideoService.MovieAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.MovieNs;

public class MovieManager : DomainService
{
    private ILogger<MovieManager> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    
    public MovieManager(IMovieRepository movieRepository,        
        IDistributedEventBus distributedEventBus,
        ILogger<MovieManager> logger )
    {
        _movieRepository = movieRepository;
        _distributedEventBus = distributedEventBus;
        _logger = logger;
    }
    
            public async Task<Movie> CreateAsync(MovieCreateDto movieCreateDto
    )
    {
        if (movieCreateDto.FirstAiredYear < 2000)
        {
            movieCreateDto.FirstAiredYear  = 2000;
        }
        // Create new movie
        var movie = new Movie(
            id: GuidGenerator.Create(),
            name: movieCreateDto.Name,
            firstAiredYear: movieCreateDto.FirstAiredYear,
            movieType: MediaType.Movie,
            isActive: true
        );

        // Add new movie aliases
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "name",
            idValue: movie.Name
        );
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "folder",
            idValue: movie.Name
        );
        
        foreach (var movieAlias in movieCreateDto.MovieAliases)
        {
            try
            {
                movie.AddMovieAlias(
                    id: GuidGenerator.Create(),
                    movieId: movie.Id,
                    idType: movieAlias.IdType,
                    idValue: movieAlias.IdValue);
            }
            catch { } 
        }
        
        var dbMovie = new Movie();
        
        var dbMovieList = await _movieRepository.GetByMovieName(movie.Name);
        if (dbMovieList.Count == 1)
        {
             dbMovie = dbMovieList[0];
        }
        else
        {
             dbMovie = await _movieRepository.FindByMovieNameYear(movie.Name, movie.FirstAiredYear);
        }
        
        if (dbMovie == null)
        {
            var createMovie = await _movieRepository.InsertAsync(movie, true);
            await PublishCreateMovieEvent(createMovie);
            return createMovie;
        }
        else
        {
            var update = 0;
            if (dbMovie.IsActive != movie.IsActive)
            {
                dbMovie.IsActive = movie.IsActive;
                update++;
            }

            if (dbMovie.Type != movie.Type)
            {
                dbMovie.Type = movie.Type;
                update++;
            }
       
            foreach (var movieAlias in movie.MovieAliases)
            {
                var found = false;
                foreach (var dbMovieAlias in dbMovie.MovieAliases)
                {
                    if ((dbMovieAlias.IdType == movieAlias.IdType) && (dbMovieAlias.IdValue == movieAlias.IdValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    dbMovie.AddMovieAlias(GuidGenerator.Create(),dbMovie.Id,movieAlias.IdType,movieAlias.IdValue);
                    update++;
                }
            }
            
            if (update > 0)
            {
                await _movieRepository.UpdateAsync(dbMovie);
            }

            return dbMovie;
        }
    }
    
    public async Task<Movie> CreateAsync(
        string name,
        int year,
        List<( Guid movieId, string idType, string idValue)> movieAliases,
        MediaType type = MediaType.Movie,
        bool isActive = true
    )
    {
        if (year < 2000)
        {
            year = 2000;
        }
        // Create new movie
        Movie movie = new Movie(
            id: GuidGenerator.Create(),
            name: name,
            firstAiredYear: year,
            movieType: type,
            isActive: isActive
        );

        // Add new movie aliases
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "name",
            idValue: movie.Name
        );
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "folder",
            idValue: movie.Name
        );
        
        foreach (var movieAlias in movieAliases)
        {
            try
            {
                movie.AddMovieAlias(
                    id: GuidGenerator.Create(),
                    movieId: movie.Id,
                    idType: movieAlias.idType,
                    idValue: movieAlias.idValue);
            }
            catch { } 
        }
        
        var dbMovie = new Movie();
        
        var dbMovieList = await _movieRepository.GetByMovieName(movie.Name);
        if (dbMovieList.Count == 1)
        {
             dbMovie = dbMovieList[0];
        }
        else
        {
             dbMovie = await _movieRepository.FindByMovieNameYear(movie.Name, movie.FirstAiredYear);
        }
        
        if (dbMovie == null)
        {
            var createMovie = await _movieRepository.InsertAsync(movie, true);
            await PublishCreateMovieEvent(createMovie);
            return createMovie;
        }
        else
        {
            var update = 0;
            if (dbMovie.IsActive != movie.IsActive)
            {
                dbMovie.IsActive = movie.IsActive;
                update++;
            }

            if (dbMovie.Type != movie.Type)
            {
                dbMovie.Type = movie.Type;
                update++;
            }
       
            foreach (var movieAlias in movieAliases)
            {
                var found = false;
                foreach (var dbMovieAlias in dbMovie.MovieAliases)
                {
                    if ((dbMovieAlias.IdType == movieAlias.idType) && (dbMovieAlias.IdValue == movieAlias.idValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    dbMovie.AddMovieAlias(GuidGenerator.Create(),dbMovie.Id,movieAlias.idType,movieAlias.idValue);
                    update++;
                }
            }
            
            if (update > 0)
            {
                await _movieRepository.UpdateAsync(dbMovie);
            }

            return dbMovie;
        }
    }
    
    public async Task<Movie> CreateAsync(
        string name,
        int year,
        List<( string idType, string idValue)> movieAliases,
        MediaType type = MediaType.Movie,
        MediaStatus status = MediaStatus.New,
        bool isActive = true
    )
    {
        // Create new movie
        Movie movie = new Movie(
            id: GuidGenerator.Create(),
            name: name,
            firstAiredYear: year,
            movieType: type,
            status: status,
            isActive: isActive
        );
        
        movie.MovieAliases = new List<MovieAlias>();
            
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "name",
            idValue: movie.Name.ToLower()
        );
        movie.AddMovieAlias(
            id: GuidGenerator.Create(),
            movieId: movie.Id,
            idType: "folder",
            idValue: movie.Name
        );
        
        var dbMovie = await _movieRepository.FindByMovieNameYear(name, year);
        if (dbMovie == null)
        {
            var createMovie = await _movieRepository.InsertAsync(movie, true);
            return createMovie;
        }
        else   // Movie maybe updated
        {
            var updateCnt = 0;
            var updateMovie = dbMovie;
            if (updateMovie.IsActive != isActive)
            {
                dbMovie.IsActive = isActive;
                updateCnt++;
            }

            if (updateMovie.MovieStatus != status)
            {
                dbMovie.MovieStatus = status;
                updateCnt++;
            } 
            
            // check for movie alias adds or updates
            foreach (var movieAlias in movie.MovieAliases )
            {
                var found = false;
                foreach (var dbMovieAlias in dbMovie.MovieAliases)
                {
                    if ((dbMovieAlias.IdType == movieAlias.IdType) && (dbMovieAlias.IdValue == movieAlias.IdValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    dbMovie.AddMovieAlias(GuidGenerator.Create(),dbMovie.Id,movieAlias.IdType,movieAlias.IdValue);
                    updateCnt++;
                }
            }

            if (updateCnt > 0)
            {
                await _movieRepository.UpdateAsync(dbMovie);
            }
            return dbMovie;
        }
    }
    
    public async Task<Movie> CreateUpdateMovieAsync(
        string name,
        int year,
        List<MovieAlias>
            movieAliases,
        MediaType type = MediaType.Movie,
        MediaStatus status = MediaStatus.New,
        bool isActive = true
    )
    {
        // Create new movie
        var movie = new Movie(
            id: GuidGenerator.Create(),
            name: name,
            firstAiredYear: year,
            movieType: type,
            status: status,
            isActive: isActive
        );

        if ((movieAliases == null) || (movieAliases.Count == 0))
        {
            movie.MovieAliases = new List<MovieAlias>();
            
        }
        else
        {
            foreach (var movieAlias in movieAliases)
            {
                movie.AddMovieAlias(
                    id: GuidGenerator.Create(),
                    movieId: movie.Id,
                    idType: movieAlias.IdType,
                    idValue: movieAlias.IdValue
                );
            }
        }

        var found1 = false;
        var found2 = false;
        foreach (var movieAlias in movie.MovieAliases)
        {
            if (movieAlias.IdType == "name")
            {
                found1 = true;
            }
            if (movieAlias.IdType == "name")
            {
                found2 = true;
            }
        }

        if (found1 == false)
        {
            movie.AddMovieAlias(
                id: GuidGenerator.Create(),
                movieId: movie.Id,
                idType: "name",
                idValue: movie.Name.ToLower()
            );
        }
        
        if (found2 == false)
        {
            movie.AddMovieAlias(
                id: GuidGenerator.Create(),
                movieId: movie.Id,
                idType: "folder",
                idValue: movie.Name
            );
        }

        var dbMovie = await _movieRepository.FindByMovieNameYear(name, year);
        if (dbMovie == null)
        {
            var createMovie = await _movieRepository.InsertAsync(movie, true);
            return createMovie;
        }
        else   // Movie maybe updated
        {
            var updateCnt = 0;
            var updateMovie = dbMovie;
            if (updateMovie.IsActive != isActive)
            {
                dbMovie.IsActive = isActive;
                updateCnt++;
            }

            if (updateMovie.MovieStatus != status)
            {
                dbMovie.MovieStatus = status;
                updateCnt++;
            } 
            
            // check for movie alias adds or updates
            foreach (var movieAlias in movieAliases)
            {
                var found = false;
                foreach (var dbMovieAlias in dbMovie.MovieAliases)
                {
                    if ((dbMovieAlias.IdType == movieAlias.IdType) && (dbMovieAlias.IdValue == movieAlias.IdValue))
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    dbMovie.AddMovieAlias(GuidGenerator.Create(),dbMovie.Id,movieAlias.IdType,movieAlias.IdValue);
                    updateCnt++;
                }
            }

            if (updateCnt > 0)
            {
                await _movieRepository.UpdateAsync(dbMovie);
            }
            return dbMovie;
        }
    }

    public async Task SetInActiveAsync(Guid id)
    {
       var dbMovie = await _movieRepository.GetAsync(id);
       dbMovie.IsActive = false;
       await _movieRepository.UpdateAsync(dbMovie, autoSave: true);
    }

    public async Task<Movie> AcceptTraktMovieAsync(string traktId, 
        string slug, 
        string name, 
        int firstAiredYear, 
        List<MovieAlias> movieAliases)
    {
        var movieout =await CreateUpdateMovieAsync(name, firstAiredYear, movieAliases);
        return movieout;
    }

    public async Task AckTraktMovieAsync(TraktMovieAcknowledgeEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktMovieIdNotGuid);
        }

        var secondTryMovie = await _movieRepository.FindByMovieNameYear(eventData.Name,eventData.Year);
        if (secondTryMovie != null)
        {
                secondTryMovie.EventStatus = FileStatus.Accepted;
                await _movieRepository.UpdateAsync(secondTryMovie, true); 
        }
    }
    
    private async Task PublishCreateMovieEvent(Movie input)
    {
        // Publish Video Movie creation event 
        await _distributedEventBus.PublishAsync(new MovieCreatedEto
        {
            MovieId = input.Id,
            Name = input.Name,
            FirstAiredYear = input.FirstAiredYear,
            Type = input.Type,
            MovieAliases = GetMovieAliasEtoList(input.MovieAliases)
        });
    }
    
    private List<MovieAliasCreatedEto> GetMovieAliasEtoList(List<MovieAlias> seriesAliases)
    {
        var etoList = new List<MovieAliasCreatedEto>();
        foreach (var oItem in seriesAliases)
        {
            etoList.Add(new MovieAliasCreatedEto()
            {
                MovieAliasId = oItem.Id,
                MovieId = oItem.MovieId,
                IdType = oItem.IdType,
                IdValue = oItem.IdValue
            });
        }

        return etoList;
    }
}
