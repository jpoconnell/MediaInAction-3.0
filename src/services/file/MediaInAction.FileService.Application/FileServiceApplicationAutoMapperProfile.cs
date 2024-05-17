using AutoMapper;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.FileService.FileEntryNs;

namespace MediaInAction.FileService
{
    public class FileServiceApplicationAutoMapperProfile : Profile
    {
        public FileServiceApplicationAutoMapperProfile()
        {
            CreateMap<FileEntry, FileEntryDto>();
        }
    }
}
