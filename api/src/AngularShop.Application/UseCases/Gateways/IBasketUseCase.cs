using System.Threading.Tasks;
using AngularShop.Application.Dtos.Basket;

namespace AngularShop.Application.UseCases.Gateways
{
    public interface IBasketUseCase
    {
        Task<BasketResponse> GetBasketAsync(string basketId);
        Task<BasketResponse> UpdateBasketAsync(BasketRequest basketRequest);
        Task<bool> RemoveBasketAsync(string basketId);
    }
}