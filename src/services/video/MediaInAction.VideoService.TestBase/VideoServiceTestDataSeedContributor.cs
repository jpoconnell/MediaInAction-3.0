using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService;

public class VideoServiceTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly LoadSeriesList _loadSeriesList;
    private readonly LoadTorrentList _loadTorrentList;
    private readonly LoadEpisodeList _loadEpisodeList;
    private readonly LoadMovieList _loadMovieList;
    private readonly LoadToBeMappedList _loadToBeMappedList;
    private readonly LoadFileEntryList _loadFileEntryList;
    
    public VideoServiceTestDataSeedContributor(
        LoadSeriesList loadSeriesList,
        LoadTorrentList loadTorrentList,
        LoadEpisodeList loadEpisodeList,
        LoadMovieList loadMovieList,
        LoadToBeMappedList loadToBeMappedList,
        LoadFileEntryList loadFileEntryList)
    {
        _loadSeriesList = loadSeriesList;
        _loadTorrentList =  loadTorrentList;
        _loadEpisodeList = loadEpisodeList;
        _loadMovieList = loadMovieList;
        _loadToBeMappedList = loadToBeMappedList;
        _loadFileEntryList = loadFileEntryList;
    }
    
    public Task SeedAsync(DataSeedContext context)
    {
        SeedTestVideoServiceAsync();

        return Task.CompletedTask;
    }

    private void SeedTestVideoServiceAsync()
    {
        SeedSeriesAsync();
        SeedEpisodeAsync();
        SeedMovieAsync();
        SeedToBeMappedAsync();
        SeedFileEntryAsync();
        SeedTorrentAsync();
    }

    private async Task SeedSeriesAsync()
    {
        var rowCnt = await _loadSeriesList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadSeriesList.LoadSeriesData();
    }
    
    private async Task SeedTorrentAsync()
    {
        var rowCnt = await _loadTorrentList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadTorrentList.LoadTorrentData();
    }
    
    private async Task SeedToBeMappedAsync()
    {
        var rowCnt = await _loadToBeMappedList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadToBeMappedList.LoadToBeMappedData();
    }
    
    private async Task SeedEpisodeAsync()
    {
        var rowCnt = await _loadEpisodeList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadEpisodeList.LoadEpisodeData();
    }

    private async Task SeedMovieAsync()
    {
        var rowCnt = await _loadMovieList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadMovieList.LoadMovieData();
    }
    
    private async Task SeedFileEntryAsync()
    {
        var rowCnt = await _loadFileEntryList.GetCount();

        if (rowCnt > 0)
        {
            return;
        }
        await _loadFileEntryList.LoadFileEntryData();
    }
}
