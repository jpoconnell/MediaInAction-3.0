using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodsNs;
using MediaInAction.FileService.FileMethodsNs.Dtos;


namespace MediaInAction.FileService.FileMethodNs;

public class FileMethodAppService : FileServiceAppService, IFileMethodAppService
{
    private readonly IEnumerable<IFileMethod> _fileMethods;

    public FileMethodAppService(IEnumerable<IFileMethod> fileMethods)
    {
        this._fileMethods = fileMethods;
    }

    public Task<List<FileMethod>> GetListAsync()
    {
        return Task.FromResult(
                _fileMethods
                    .Select(p => new FileMethod { Name = p.Name })
                    .ToList()
            );
    }
}
