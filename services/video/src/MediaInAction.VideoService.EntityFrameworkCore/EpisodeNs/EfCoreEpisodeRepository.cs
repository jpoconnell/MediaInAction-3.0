using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs
{
    public class EfCoreEpisodeRepository
        : EfCoreRepository<VideoServiceDbContext , Episode, Guid>,
            IEpisodeRepository
    {
        public EfCoreEpisodeRepository(
            IDbContextProvider<VideoServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        
        public async Task<List<Episode>> GetEpisodesByUserId(
            Guid userId,
            ISpecification<Episode> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await (await GetDbSetAsync())
                    .IncludeDetails(includeDetails)
                    .Where(spec.ToExpression())
                    .ToListAsync(GetCancellationToken(cancellationToken));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<Episode>> GetListPagedAsync(ISpecification<Episode> spec, 
            int skipCount, 
            int maxResultCount, 
            string sorting,
            bool includeDetails = false, 
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.AiredDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Episode>> GetEpisodesBySeriesId(
            Guid seriesId,
            ISpecification<Episode> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(q => q.SeriesId == seriesId)
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.AiredDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        
        public async Task<Episode> FindBySeriesIdSeasonEpisodeNum(
            Guid seriesId, 
            int seasonNum, 
            int episodeNum,
            bool includeDetails = false)
        {   
            try
            {
                var dbSet = await GetDbSetAsync();
                return await dbSet
                    .IncludeDetails(includeDetails)
                    .Where(q => q.SeriesId == seriesId &&  q.SeasonNum == seasonNum &&
                                q.EpisodeNum == episodeNum )
                    .FirstAsync();
            }
            catch 
            {
                return null;
            }
        }

        public async Task<List<Episode>>  GetListAsync(
            ISpecification<Episode> spec, 
            bool includeDetails = true, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await (await GetDbSetAsync())
                    .IncludeDetails(includeDetails)
                    .Where(spec.ToExpression())
                    .ToListAsync(GetCancellationToken(cancellationToken));
            }
            catch 
            {
                return null;
            }
        }

        public async Task<List<Episode>> GetAllEpisodesByStatus(
            MediaStatus status,
            ISpecification<Episode> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {

            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(q => q.MediaStatus == status)
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.AiredDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        
        public async Task<List<Episode>> GetEpisodesBySeriesSeasonAsync(
            Guid seriesId,
            int seasonNum,
            ISpecification<Episode> spec,
            bool includeDetails = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(q => q.SeriesId == seriesId && q.SeasonNum == seasonNum)
                .Where(spec.ToExpression())
                .OrderByDescending(o => o.AiredDate)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        
        public async Task<List<Episode>> GetDashboardAsync(
            ISpecification<Episode> spec,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .IncludeDetails(includeDetails)
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public Task<Episode> GetBySlugSeasonEpisode(string slug, int season, int episode)
        {
            throw new NotImplementedException();
        }

        public Task<Episode> GetByIdAsync(Guid episodeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EpisodeAlias>> GetBySlug(string requestSlug)
        {
            throw new NotImplementedException();
        }
    }
}