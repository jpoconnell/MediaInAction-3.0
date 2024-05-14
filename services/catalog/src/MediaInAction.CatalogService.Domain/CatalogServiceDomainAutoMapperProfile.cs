using AutoMapper;
using MediaInAction.CatalogService.Products;

namespace MediaInAction.CatalogService
{
    public class CatalogServiceDomainAutoMapperProfile : Profile
    {
        public CatalogServiceDomainAutoMapperProfile()
        {
            CreateMap<Product, ProductEto>();
        }
    }
}
