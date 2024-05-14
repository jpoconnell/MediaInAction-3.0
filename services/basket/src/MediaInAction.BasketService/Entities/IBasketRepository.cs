using Volo.Abp.Domain.Repositories;

namespace MediaInAction.BasketService.Entities;

public interface IBasketRepository : IRepository
{
    Task<Basket> GetAsync(Guid id);

    Task UpdateAsync(Basket basket);
}