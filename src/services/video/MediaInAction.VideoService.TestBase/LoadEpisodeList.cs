using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadEpisodeList : ISingletonDependency
{
    private readonly List<EpisodeCreateDto> _episodeList;
    private readonly Random _random = new();
    private readonly IEpisodeRepository _episodeRepository;
    private readonly EpisodeManager _episodeManager;
    
    public LoadEpisodeList(IEpisodeRepository episodeRepository, 
        EpisodeManager episodeManager)
    {
        _episodeList = new List<EpisodeCreateDto>();
        _episodeRepository = episodeRepository;
        _episodeManager = episodeManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/episode.json"))
        {
            string json = r.ReadToEnd();
            _episodeList = JsonConvert.DeserializeObject<List<EpisodeCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var episodeList =await _episodeRepository.GetListAsync();
        return episodeList.Count;
    }
    
    public async Task  LoadEpisodeData()
    {
        foreach (var episode in _episodeList)
        {
            await _episodeManager.CreateAsync(episode);
        }
    }
}