using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService;

public class LoadShowList : ISingletonDependency
{
    private readonly List<TraktShowCreateDto> _traktShowCreateDtos;
    private readonly Random _random = new();
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly TraktShowManager _traktShowManager;
    
    public LoadShowList(ITraktShowRepository traktShowRepository, 
        TraktShowManager traktShowManager)
    {
        _traktShowCreateDtos = new List<TraktShowCreateDto>();
        _traktShowRepository = traktShowRepository;
        _traktShowManager = traktShowManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.TraktService.TestBase/TestData/traktShow.json"))
        {
            string json = r.ReadToEnd();
            _traktShowCreateDtos = JsonConvert.DeserializeObject<List<TraktShowCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var traktShowList =await _traktShowRepository.GetListAsync();
        return traktShowList.Count;
    }
    
    
    public async Task  LoadShowData()
    {
        foreach (var traktShow in _traktShowCreateDtos)
        {
            await _traktShowManager.CreateAsync(traktShow);
        }
    }
}