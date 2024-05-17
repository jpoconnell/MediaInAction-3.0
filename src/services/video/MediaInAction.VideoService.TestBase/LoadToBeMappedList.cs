using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadToBeMappedList : ISingletonDependency
{
    private readonly List<ToBeMappedCreateDto> _toBeMappedCreateList;
    private readonly Random _random = new();
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;
    
    public LoadToBeMappedList(IToBeMappedRepository toBeMappedRepository, 
        ToBeMappedManager toBeMappedManager)
    {
        _toBeMappedCreateList = new List<ToBeMappedCreateDto>();
        _toBeMappedRepository = toBeMappedRepository;
        _toBeMappedManager = toBeMappedManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/toBeMapped.json"))
        {
            string json = r.ReadToEnd();
            _toBeMappedCreateList = JsonConvert.DeserializeObject<List<ToBeMappedCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var toBeMappedList =await _toBeMappedRepository.GetListAsync();
        return toBeMappedList.Count;
    }
    
    public async Task LoadToBeMappedData()
    {
        foreach (var toBeMapped in _toBeMappedCreateList)
        {
            await _toBeMappedManager.CreateAsync(toBeMapped);
        }
    }
}
