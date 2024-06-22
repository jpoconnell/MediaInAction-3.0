using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.Products;
using JetBrains.Annotations;

namespace MediaInAction.EmbyService.Services;

public interface IEmbyProductService
{
    [ItemNotNull]
    Task<ProductDto> GetAsync(Guid productId);
}