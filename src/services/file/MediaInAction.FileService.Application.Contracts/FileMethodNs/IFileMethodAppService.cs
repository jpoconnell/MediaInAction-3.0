using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.FileMethodNs;

public interface IFileMethodAppService : IApplicationService
{
    Task<List<FileMethod>> GetListAsync();
}
