using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{
    public class TraktServicesConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        
        public TraktServicesConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ClientId = configuration["Trakt:ClientId"];
            ClientSecret = configuration["Trakt:ClientSecret"];
            AccessToken = configuration["Trakt:AccessToken"];
            RefreshToken = configuration["Trakt:RefreshToken"];
        }
    }
}
