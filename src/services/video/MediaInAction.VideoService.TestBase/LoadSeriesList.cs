using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadSeriesList : ISingletonDependency
{
    private readonly List<SeriesCreateDto> _seriesList;
    private readonly Random _random = new();
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesManager _seriesManager;
    
    public LoadSeriesList(ISeriesRepository seriesRepository, 
        SeriesManager seriesManager)
    {
        _seriesList = new List<SeriesCreateDto>();
        _seriesRepository = seriesRepository;
        _seriesManager = seriesManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/series.json"))
        {
            string json = r.ReadToEnd();
            _seriesList = JsonConvert.DeserializeObject<List<SeriesCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var seriesList =await _seriesRepository.GetListAsync();
        return seriesList.Count;
    }

    public object GetRandomSeriesList(int i)
    {
        throw new NotImplementedException();
    }
    
    public async Task  LoadSeriesData()
    {
        foreach (var series in _seriesList)
        {
            await _seriesManager.CreateAsync(series);
        }
    }
}