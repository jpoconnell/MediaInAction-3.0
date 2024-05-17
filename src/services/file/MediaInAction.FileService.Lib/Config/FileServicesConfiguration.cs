using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService.Lib.Config
{
    public class FileServicesConfiguration
    {
        public string UnCompressedFolder { get; set; }
        public string CompressedFolder { get; set; }
        public string LibraryTv { get; set; }
        public string LibraryMovie { get; set; }
        
        public FileServicesConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            UnCompressedFolder = configuration["MediaLocations:UnCompressed"];
            CompressedFolder = configuration["MediaLocations:Compressed"];
            LibraryTv = configuration["MediaLocations:LibraryTV"];
            LibraryMovie = configuration["MediaLocations:LibraryMovie"];
        }
    }
}
