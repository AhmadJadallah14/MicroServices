using Basket.Api.Entity;

namespace Basket.Api.Repositries
{
    public interface IBasketRepositry
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName); 
    }
}
