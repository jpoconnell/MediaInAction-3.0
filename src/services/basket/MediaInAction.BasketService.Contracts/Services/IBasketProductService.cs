using System;
using System.Threading.Tasks;
using MediaInAction.CatalogService.Products;
using JetBrains.Annotations;

namespace MediaInAction.BasketService.Services;

public interface IBasketProductService
{
    [ItemNotNull]
    Task<ProductDto> GetAsync(Guid productId);
}