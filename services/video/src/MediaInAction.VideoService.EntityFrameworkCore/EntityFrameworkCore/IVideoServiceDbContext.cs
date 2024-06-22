using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.TorrentNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.EntityFrameworkCore
{
    [ConnectionStringName(VideoServiceDbProperties.ConnectionStringName)]
    public interface IVideoServiceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Series> SeriesList { get; }
        DbSet<Movie> Movies { get; }
        DbSet<Episode> Episodes { get; }
        DbSet<FileEntry> FileEntries { get; }
        DbSet<ToBeMapped> ToBeMappeds { get; }
        DbSet<Torrent> Torrents { get; }
    }
}