using AutoMapper;
using MediaInAction.CatalogService.Grpc;
using MediaInAction.CatalogService.Products;

namespace MediaInAction.CatalogService
{
    public class CatalogServiceApplicationAutoMapperProfile : Profile
    {
        public CatalogServiceApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
