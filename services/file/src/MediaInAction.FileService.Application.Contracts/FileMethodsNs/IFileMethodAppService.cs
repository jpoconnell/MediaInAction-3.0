using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.FileService.FileMethodsNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.FileService.FileMethodsNs;

public interface IFileMethodAppService : IApplicationService
{
    Task<List<FileMethod>> GetListAsync();
}
