using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService.Config
{
    public class DelugeServicesConfiguration
    {
        public string DelugeUrl { get; set; }
        public string DelugePassword { get; set; }


        
        public DelugeServicesConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            DelugeUrl = configuration["Deluge:Url"];
            DelugePassword = configuration["Deluge:Password"];
        }
    }
}
