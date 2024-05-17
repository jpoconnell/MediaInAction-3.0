using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadMovieList : ISingletonDependency
{
    private readonly List<MovieCreateDto> _movieList;
    private readonly Random _random = new();
    private readonly IMovieRepository _movieRepository;
    private readonly MovieManager _movieManager;
    
    public LoadMovieList(IMovieRepository movieRepository, 
        MovieManager movieManager)
    {
        _movieList = new List<MovieCreateDto>();
        _movieRepository = movieRepository;
        _movieManager = movieManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/movie.json"))
        {
            string json = r.ReadToEnd();
            _movieList = JsonConvert.DeserializeObject<List<MovieCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var movieList =await _movieRepository.GetListAsync();
        return movieList.Count;
    }
    
    
    public async Task  LoadMovieData()
    {
        foreach (var movie in _movieList)
        {
            await _movieManager.CreateAsync(movie);
        }
    }
}