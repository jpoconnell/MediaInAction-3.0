using DelugeRPCClient.Net;
using MediaInAction.DelugeService.Config;
using Microsoft.Extensions.Logging;

namespace MediaInAction.DelugeService;

public class DelugeService : IDelugeService
{
    private DelugeClient _delugeClient;
    private readonly ILogger<DelugeService> _logger;
    private readonly DelugeServicesConfiguration _delugeConfig;
    
    public DelugeService(
        ILogger<DelugeService> logger,
        DelugeServicesConfiguration delugeConfig
        )
    {
        _logger = logger;
        _delugeConfig = delugeConfig;
        _delugeClient = new DelugeClient( _delugeConfig.DelugeUrl, _delugeConfig.DelugePassword);
    }

    public DelugeClient GetClient()
    {
        // setup Deluge Client
        var delugeUrl = _delugeConfig.DelugeUrl;
        var delugePassword = _delugeConfig.DelugePassword;
        _delugeClient = new DelugeClient( _delugeConfig.DelugeUrl, _delugeConfig.DelugePassword);
        return _delugeClient;
    }
}
