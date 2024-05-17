using AutoMapper;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyMovieNs;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;

namespace MediaInAction.EmbyService
{
    public class EmbyServiceApplicationAutoMapperProfile : Profile
    {
        public EmbyServiceApplicationAutoMapperProfile()
        {
            CreateMap<EmbyMovie, EmbyMovieDto>();
            CreateMap<EmbyShow, EmbyShowDto>();
            CreateMap<EmbyEpisode, EmbyEpisodeDto>();
        }
    }
}