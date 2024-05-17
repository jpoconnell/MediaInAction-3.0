using System.Threading.Tasks;

namespace MediaInAction.EmbyService.FolderServices
{
    public interface IFolderService
    {
        Task UpdateAddFromDto(EmbyFolderDto folder);
    }
}