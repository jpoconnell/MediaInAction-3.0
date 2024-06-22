using System;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.ParserNs;

public class ParserDto
{
    public ListType ListName { get; set; }
    public MediaType MediaType { get; set; }
    public ParseType ParseType { get; set; }
    public Guid Link { get; set; }
    public string IncomingName { get; set; }
    public string IncomingFullPath { get; set; }
    public string OutputName { get; set; }
    public string OutFullPath { get; set; }
    public Guid SeriesLink { get; set; }
    public string SeriesName { get; set; }
    public string Resolution { get; set; }
    public string Extn { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public string Directory { get; set; }
    public string Category { get; set; }
    public string EpisodeId { get; set; }
    public string EpisodeType { get; set; }
    public string EpisodeName { get; set; }
    public bool ToBeMapped { get; set; }
    public string SeasonEpisode { get; set; }
    public string CleanFileName { get; set; }
}