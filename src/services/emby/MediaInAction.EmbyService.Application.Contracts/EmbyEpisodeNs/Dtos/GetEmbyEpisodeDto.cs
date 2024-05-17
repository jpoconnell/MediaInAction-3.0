namespace MediaInAction.EmbyService.EmbyEpisodeNs.Dtos;

public class GetEmbyEpisodeDto 
{
    public string Slug { get; set; }
    public int Season { get; set; }
    public int Episode { get; set; }
}
