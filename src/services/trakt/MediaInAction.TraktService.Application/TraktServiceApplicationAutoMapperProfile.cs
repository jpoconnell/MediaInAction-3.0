using AutoMapper;
using MediaInAction.TraktService.EpisodeNs;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;

namespace MediaInAction.TraktService
{
    public class TraktServiceApplicationAutoMapperProfile : Profile
    {
        public TraktServiceApplicationAutoMapperProfile()
        {
            CreateMap<TraktMovieEto, TraktMovieDto>();
            CreateMap<TraktShowEto, TraktShowDto>();
            CreateMap<TraktEpisodeEto, TraktEpisodeDto>();
        }
    }
}
