using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.TraktMethods;

public class TraktMethodResolver : ITransientDependency
{
    private readonly IEnumerable<ITraktMethod> _traktMethods;
    private readonly ILogger<TraktMethodResolver> _logger;

    public TraktMethodResolver(IEnumerable<ITraktMethod> traktMethods, 
        ILogger<TraktMethodResolver> logger)
    {
        _traktMethods = traktMethods;
        _logger = logger;
    }

    public ITraktMethod Resolve(string traktMethodName)
    {
        var traktMethod = _traktMethods.FirstOrDefault(q => q.Name.Equals(traktMethodName, StringComparison.InvariantCultureIgnoreCase));
        if (traktMethod == null)
        {
            _logger.LogError($"Couldn't find  method with type:{traktMethodName}");
            throw new ArgumentException(" method not found", traktMethodName);
        }

        return traktMethod;
    }
}