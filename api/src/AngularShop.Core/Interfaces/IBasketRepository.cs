using System.Threading.Tasks;
using AngularShop.Core.Entities;

namespace AngularShop.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> GetAsync(string basketId);
        Task<Basket> UpdateAsync(Basket basket);
        Task<bool> RemoveAsync(string basketId);
    }
}