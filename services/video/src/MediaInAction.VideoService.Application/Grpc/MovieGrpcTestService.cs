using System.Threading.Tasks;
using Grpc.Core;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieNs;
using Microsoft.Extensions.Logging;
using VideoService.Movie.GrpcServer;

namespace MediaInAction.VideoService.Grpc;

public class MovieGrpcTestService : MovieGrpcService.MovieGrpcServiceBase
{
    private readonly ILogger<MovieGrpcTestService> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieAliasRepository _movieAliasRepository;
    private readonly MovieManager _movieManager;
    
    public MovieGrpcTestService(ILogger<MovieGrpcTestService> logger, 
        IMovieRepository movieRepository,
        IMovieAliasRepository movieAliasRepository,
        MovieManager movieManager)
    {
        _logger = logger;
        _movieRepository = movieRepository;
        _movieAliasRepository = movieAliasRepository;
        _movieManager = movieManager;
    }

    public override async Task<MovieModel> CreateNewMovie(MovieModel request, ServerCallContext context)
    {
        var movieCreateDto = TranslateMovieGrpc(request);
        var response = await _movieManager.CreateAsync(movieCreateDto);

        var movieModel = TranslateMovie(response);
        return movieModel;
    }
    
    public override async Task SearchMovies(SearchRequest request, IServerStreamWriter<MovieModel> responseStream, ServerCallContext context)
    {
        var movieList = await  _movieRepository.GetListAsync();
        foreach (var movie in movieList)
        {
            bool match = false;
            if (request.Slug.Length > 0)
            {
                foreach (var MovieAlias in movie.MovieAliases)
                {
                    if ((MovieAlias.IdType == "slug") && (MovieAlias.IdValue.ToUpper().Contains(request.Slug.ToUpper())))
                    {
                        match = true;
                    }
                }
            }
            if (request.Name.Length > 0)
            {
                if (movie.Name.ToUpper().Contains(request.Name.ToUpper()))
                {
                    match = true;
                }
            }
            if (match)
            {
                await Task.Delay(1000);
                var movieModel = TranslateMovie(movie);
                await responseStream.WriteAsync(movieModel);
            }
        }
    }
    
    public override async Task<MovieModel> UpdateMovie(MovieModel request, ServerCallContext context)
    {
        var movieList = await _movieRepository.GetBySlugAsync(request.Slug);
        if (movieList.Count == 1)
        {
            var movie = movieList[0];
            var movieCreateDto = new MovieCreateDto();
            movieCreateDto.Name = request.Name;
            movieCreateDto.FirstAiredYear = request.Year;
            var createdMovie = await _movieManager.UpdateAsync(movieCreateDto);
            var updateMovie = await _movieRepository.GetByIdAsync(movie.Id);
            return TranslateMovie(movie);
        }
        else if (movieList.Count == 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Movie with ID={request.Slug} is not found."));
        }
        else
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Movie with ID={request.Slug} to many found."));
        }
    }
    
    private MovieModel TranslateMovie(MovieNs.Movie movie)
    {
        var movieModel = new MovieModel();
        movieModel.Name = movie.Name;
        movieModel.Year = movie.FirstAiredYear;
        
        foreach (var movieAlias in movie.MovieAliases)
        {
            var newMovieAlias = new MovieAliasModel();
            newMovieAlias.IdType = movieAlias.IdType;
            newMovieAlias.IdValue = movieAlias.IdValue;
            movieModel.Aliases.Add(newMovieAlias);
            if (movieAlias.IdType == "Slug")
            {
                movieModel.Slug = movieAlias.IdValue;
            }
        }
        return movieModel;
    }

    private MovieCreateDto TranslateMovieGrpc(MovieModel request)
    {
        var MovieCreateDto = new MovieCreateDto();
        MovieCreateDto.Name = request.Name;
        MovieCreateDto.FirstAiredYear = request.Year;
        foreach (var movieAlias in request.Aliases)
        {
            MovieCreateDto.MovieAliases.Add(new MovieAliasCreateDto
            {
                IdType = movieAlias.IdType,
                IdValue = movieAlias.IdValue,
            });
        }
        return MovieCreateDto;
    }
}