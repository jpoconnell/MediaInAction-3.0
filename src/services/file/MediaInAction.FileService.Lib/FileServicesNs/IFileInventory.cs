using System.Threading.Tasks;

namespace MediaInAction.FileService.Lib.FileServicesNs;

    public interface IFileInventory
    {
        Task<bool> GetFiles();
    }
