using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadFileEntryList : ISingletonDependency
{
    private readonly List<FileEntryCreateDto> _fileEntryCreateList;
    private readonly Random _random = new();
    private readonly IFileEntryRepository _fileEntryRepository;
    private readonly FileEntryManager _fileEntryManager;
    
    public LoadFileEntryList(IFileEntryRepository fileEntryRepository, 
        FileEntryManager fileEntryManager)
    {
        _fileEntryCreateList = new List<FileEntryCreateDto>();
        _fileEntryRepository = fileEntryRepository;
        _fileEntryManager = fileEntryManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/fileEntry.json"))
        {
            string json = r.ReadToEnd();
            _fileEntryCreateList = JsonConvert.DeserializeObject<List<FileEntryCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var fileEntryList =await _fileEntryRepository.GetListAsync();
        return fileEntryList.Count;
    }
    
    public async Task  LoadFileEntryData()
    {
        foreach (var fileEntry in _fileEntryCreateList)
        {
            await _fileEntryManager.CreateAsync(fileEntry);
        }
    }
}
