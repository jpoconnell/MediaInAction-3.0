using AutoMapper;
using MediaInAction.EmbyService.EmbyEpisodeNs;
using MediaInAction.EmbyService.EmbyEpisodesNs;
using MediaInAction.EmbyService.EmbyMoviesNs.Dtos;
using MediaInAction.EmbyService.EmbyShowNs.Dtos;
using MediaInAction.EmbyService.EmbyShowsNs;
using EmbyMovieDto = MediaInAction.EmbyService.EmbyMoviesNs.EmbyMovieDto;

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