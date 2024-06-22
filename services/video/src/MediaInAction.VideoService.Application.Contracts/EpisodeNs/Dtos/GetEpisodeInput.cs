using System;

namespace MediaInAction.VideoService.EpisodeNs.Dtos;

public class GetEpisodeInput
{
    public Guid SeriesId  { get; set; }
    public int  SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
}