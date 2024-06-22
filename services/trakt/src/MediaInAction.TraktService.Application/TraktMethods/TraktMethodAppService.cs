using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaInAction.TraktService.TraktMethods;

public class TraktMethodAppService : TraktServiceAppService, ITraktMethodAppService
{
    private readonly IEnumerable<ITraktMethod> _traktMethods;

    public TraktMethodAppService(IEnumerable<ITraktMethod> traktMethods)
    {
        this._traktMethods = traktMethods;
    }

    public Task<List<TraktMethodDto>> GetListAsync()
    {
        return Task.FromResult(
                _traktMethods
                    .Select(p => new TraktMethodDto { Name = p.Name })
                    .ToList()
            );
    }
}
