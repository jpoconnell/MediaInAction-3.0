using MediaInAction.TraktService.TraktMethods;
using MediaInAction.PublicWeb.TraktMethods;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.PublicWeb.ServiceProviders;

public class TraktMethodProvider : ITransientDependency
{
    protected ITraktMethodAppService TraktMethodAppService { get; }

    private readonly TraktMethodUiOptions _options;

    public TraktMethodProvider(
        ITraktMethodAppService paymentMethodAppService,
        IOptions<TraktMethodUiOptions> options)
    {
        TraktMethodAppService = paymentMethodAppService;
        _options = options.Value;
    }

    public async Task<List<TraktMethodViewModel>> GetTraktMethodsAsync()
    {
        var paymentMethods = await TraktMethodAppService.GetListAsync();

        return paymentMethods.Select((pm, i) => new TraktMethodViewModel
        {
            Name = pm.Name,
            IsDefault = i == 0,
            IconCss = _options.Icons.GetOrDefault(pm.Name)?? _options.DefaultIcon
        }).ToList();
    }
}

public class TraktMethodViewModel
{
    public string Name { get; set; }
    public string IconCss { get; set; }
    public bool IsDefault { get; set; }
}