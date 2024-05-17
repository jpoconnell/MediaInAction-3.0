using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktEpisodeNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService;

public class LoadEpisodeList : ISingletonDependency
{
    private readonly List<TraktEpisodeCreateDto> _traktEpisodeCreateDtos;
    private readonly Random _random = new();
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;
    
    public LoadEpisodeList(ITraktEpisodeRepository traktEpisodeRepository, 
        TraktEpisodeManager traktEpisodeManager)
    {
        _traktEpisodeCreateDtos = new List<TraktEpisodeCreateDto>();
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.TraktService.TestBase/TestData/traktEpisode.json"))
        {
            string json = r.ReadToEnd();
            _traktEpisodeCreateDtos = JsonConvert.DeserializeObject<List<TraktEpisodeCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var traktEpisodeList =await _traktEpisodeRepository.GetListAsync();
        return traktEpisodeList.Count;
    }
    
    public async Task  LoadEpisodeData()
    {
        foreach (var traktEpisode in _traktEpisodeCreateDtos)
        {
            await _traktEpisodeManager.CreateAsync(traktEpisode);
        }
    }
}