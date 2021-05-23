using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Application.Dtos.Order;
using AngularShop.Core.Entities.Order;

namespace AngularShop.Application.UseCases.Gateways
{
    public interface IOrderUseCase
    {
        Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest);

        Task<IReadOnlyList<OrderResponse>> GetOrdersFromBuyerAsync();

        Task<OrderResponse> GetOrderByIdAsync(int id);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}