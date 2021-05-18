using System.Threading.Tasks;
using AngularShop.Application.Dtos.Basket;

namespace AngularShop.Application.UseCases.Gateways
{
    public interface IBasketUseCase
    {
        Task<BasketToReturn> GetBasketAsync(string basketId);
        Task<BasketToReturn> UpdateBasketAsync(BasketRequest basketRequest);
        Task<bool> RemoveBasketAsync(string basketId);
    }
}