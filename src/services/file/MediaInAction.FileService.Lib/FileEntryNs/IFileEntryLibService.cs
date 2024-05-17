using System.Threading.Tasks;

namespace MediaInAction.FileService.FileEntryNs;

    public interface IFileEntryLibService 
    {
        Task CreateFileEntryAsync(CreateFileEntryDto rec);
        Task ResendUnAcceptedFileEntries();
    }

