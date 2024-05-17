using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class LoadTorrentList : ISingletonDependency
{
    private readonly List<TorrentCreateDto> _torrentCreateList;
    private readonly Random _random = new();
    private readonly ITorrentRepository _torrentRepository;
    private readonly TorrentManager _torrentManager;
    
    public LoadTorrentList(ITorrentRepository torrentRepository, 
        TorrentManager torrentManager)
    {
        _torrentCreateList = new List<TorrentCreateDto>();
        _torrentRepository = torrentRepository;
        _torrentManager = torrentManager;
        using (StreamReader r = new StreamReader("../../../../MediaInAction.VideoService.TestBase/TestData/torrent.json"))
        {
            string json = r.ReadToEnd();
            _torrentCreateList = JsonConvert.DeserializeObject<List<TorrentCreateDto>>(json);
        }
    }
    
    public async Task<int> GetCount()
    {
        var torrentList =await _torrentRepository.GetListAsync();
        return torrentList.Count;
    }
    
    public async Task  LoadTorrentData()
    {
        foreach (var torrent in _torrentCreateList)
        {
            await _torrentManager.CreateAsync(torrent);
        }
    }
}
