using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using MediaInAction.VideoService.TorrentNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using FileEntry = MediaInAction.VideoService.FileEntryNs.FileEntry;

namespace MediaInAction.VideoService.EntityFrameworkCore;

[ConnectionStringName(VideoServiceDbProperties.ConnectionStringName)]
public class VideoServiceDbContext : AbpDbContext<VideoServiceDbContext>, IVideoServiceDbContext
{
    public virtual DbSet<Series> SeriesList { get; set; }
    public virtual DbSet<Episode> Episodes { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<FileEntry> FileEntries { get; set; }
    public virtual DbSet<ToBeMapped> ToBeMappeds { get; set; }
    public virtual DbSet<Torrent> Torrents { get; set; }
    public virtual DbSet<SeriesAlias> SeriesAliases { get; set; }
    public virtual DbSet<EpisodeAlias> EpisodeAliases { get; set; }
    public virtual DbSet<MovieAlias> MovieAliases { get; set; }
    
    public VideoServiceDbContext(DbContextOptions<VideoServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* Include modules to your migration db context */

        modelBuilder.ConfigureVideoService();
        /* Configure your own tables/entities inside here */
    }
}
