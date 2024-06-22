using System;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.EpisodeNs;

public class UpdateEpisodeStatus
{
    public Guid Id { get; set; }
    public MediaStatus EpisodeStatus  { get; set; }
}