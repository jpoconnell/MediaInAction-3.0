using AutoMapper;
using MediaInAction.CatalogService.Grpc;
using MediaInAction.CatalogService.Products;

namespace MediaInAction.BasketService;

public class BasketServiceApplicationAutoMapperProfile : Profile
{
    public BasketServiceApplicationAutoMapperProfile()
    {
        CreateMap<ProductEto, ProductDto>();
        CreateMap<ProductResponse, ProductDto>();
    }
}