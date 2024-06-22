using AutoMapper;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;

namespace MediaInAction.VideoService
{
    public class VideoServiceApplicationAutoMapperProfile : Profile
    {
        public VideoServiceApplicationAutoMapperProfile()
        {
            CreateMap<Series, SeriesDto>();
            //CreateMap<Series, SeriesResponse>();
        }
    }
}
