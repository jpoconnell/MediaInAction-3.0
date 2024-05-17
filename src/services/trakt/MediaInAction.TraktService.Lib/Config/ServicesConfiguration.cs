using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService.Config
{
    public class ServicesConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        
        public ServicesConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            ClientId = configuration["Trakt:ClientId"];
            ClientSecret = configuration["Trakt:ClientSecret"];
            AccessToken = configuration["Trakt:AccessToken"];
            RefreshToken = configuration["Trakt:RefreshToken"];
        }
    }
}
