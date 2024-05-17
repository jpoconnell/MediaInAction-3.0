using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.FileService.FileMethodNs;

public class FileMethodResolver : ITransientDependency
{
    private readonly IEnumerable<IFileMethod> _fileMethods;
    private readonly ILogger<FileMethodResolver> _logger;

    public FileMethodResolver(IEnumerable<IFileMethod> fileMethods, ILogger<FileMethodResolver> logger)
    {
        _fileMethods = fileMethods;
        _logger = logger;
    }

    public IFileMethod Resolve(string paymentMethodName)
    {
        var paymentMethod = _fileMethods.FirstOrDefault(q => q.Name.Equals(paymentMethodName, StringComparison.InvariantCultureIgnoreCase));
        if (paymentMethod == null)
        {
            _logger.LogError($"Couldn't find Payment method with type:{paymentMethodName}");
            throw new ArgumentException("Payment method not found", paymentMethodName);
        }

        return paymentMethod;
    }
}