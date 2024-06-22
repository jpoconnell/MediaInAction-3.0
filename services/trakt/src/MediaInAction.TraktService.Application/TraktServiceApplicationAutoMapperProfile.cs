using AutoMapper;
using MediaInAction.TraktService.TraktRequests;
using Volo.Abp.AutoMapper;

namespace MediaInAction.TraktService
{
    public class TraktServiceApplicationAutoMapperProfile : Profile
    {
        public TraktServiceApplicationAutoMapperProfile()
        {
            CreateMap<TraktRequest, TraktRequestDto>();
            CreateMap<TraktRequestProduct, TraktRequestProductDto>();

            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}