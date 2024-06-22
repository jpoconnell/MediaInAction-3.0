using AutoMapper;
using MediaInAction.DelugeService.TorrentNs;
using MediaInAction.DelugeService.TorrentNs.Dtos;
using Volo.Abp.AutoMapper;

namespace MediaInAction.DelugeService
{
    public class DelugeServiceApplicationAutoMapperProfile : Profile
    {
        public DelugeServiceApplicationAutoMapperProfile()
        {
            CreateMap<Torrent, TorrentDto>();
            CreateMap<TorrentDto, Torrent>();
        }
    }
}