using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaInAction.TraktService.TraktMethods;

public class TraktMethodAppService : TraktServiceAppService, ITraktMethodAppService
{
    private readonly IEnumerable<ITraktMethod> _methods;

    public TraktMethodAppService(IEnumerable<ITraktMethod> methods)
    {
        this._methods = methods;
    }

    public Task<List<TraktMethodDto>> GetListAsync()
    {
        return Task.FromResult(
                _methods
                    .Select(p => new TraktMethodDto { Name = p.Name })
                    .ToList()
            );
    }
}
