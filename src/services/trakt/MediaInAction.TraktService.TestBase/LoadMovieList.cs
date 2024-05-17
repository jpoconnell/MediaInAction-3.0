using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService;

public class LoadMovieList : ISingletonDependency
{
    private readonly List<TraktMovieCreateDto> _traktMovieCreateDtos;
    private readonly Random _random = new();
    private readonly ITraktMovieRepository _traktMovieRepository;
    private readonly TraktMovieManager _traktMovieManager;
    
    public LoadMovieList(ITraktMovieRepository traktMovieRepository, 
        TraktMovieManager traktMovieManager)
    {
        _traktMovieCreateDtos = new List<TraktMovieCreateDto>();
        _traktMovieRepository = traktMovieRepository;
        _traktMovieManager = traktMovieManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.TraktService.TestBase/TestData/traktMovie.json"))
        {
            string json = r.ReadToEnd();
            _traktMovieCreateDtos = JsonConvert.DeserializeObject<List<TraktMovieCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var traktMovieList =await _traktMovieRepository.GetListAsync();
        return traktMovieList.Count;
    }
    
    
    public async Task  LoadMovieData()
    {
        foreach (var traktMovie in _traktMovieCreateDtos)
        {
            await _traktMovieManager.CreateAsync(traktMovie);
        }
    }
}