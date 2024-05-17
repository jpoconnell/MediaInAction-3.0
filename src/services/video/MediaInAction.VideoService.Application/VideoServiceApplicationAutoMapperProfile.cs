using AutoMapper;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.MovieNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;

namespace MediaInAction.VideoService
{
    public class VideoServiceApplicationAutoMapperProfile : Profile
    {
        public VideoServiceApplicationAutoMapperProfile()
        {
            CreateMap<FileEntry, FileEntryDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<Series, SeriesDto>();
            CreateMap<Episode, EpisodeDto>();
        }
    }
}