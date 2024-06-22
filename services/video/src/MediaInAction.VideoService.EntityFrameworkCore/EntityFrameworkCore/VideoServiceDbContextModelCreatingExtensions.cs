using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace MediaInAction.VideoService.EntityFrameworkCore;

public static class VideoServiceDbContextModelCreatingExtensions
{
    public static void ConfigureVideoService(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        // Configure all entities here. Example:

        builder.Entity<ToBeMapped>(b =>
        {
            //Configure table & schema name
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "ToBeMappeds", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(q => q.Alias)
                .IsUnique();
        });
        
        builder.Entity<Series>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "SeriesList", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(p => new { p.Name, p.FirstAiredYear })
                .IsUnique();
        });
        
        builder.Entity<Movie>(b =>
        {
            b.ToTable(VideoServiceDbProperties.DbTablePrefix + "Movies", VideoServiceDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(p => new { p.Name, p.FirstAiredYear })
                .IsUnique();
        });
    }
}