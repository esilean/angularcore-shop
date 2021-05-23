using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Application.Dtos.Order;
using AngularShop.Application.Errors;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularShop.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderUseCase _orderUseCase;
        public OrdersController(IOrderUseCase orderUseCase)
        {
            _orderUseCase = orderUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
        {
            var orderCreated = await _orderUseCase.CreateOrderAsync(orderRequest);
            if (orderCreated == null)
                return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(orderCreated);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrdersFromBuyer()
        {
            var orders = await _orderUseCase.GetOrdersFromBuyerAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrderFromBuyer(int id)
        {
            var order = await _orderUseCase.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new ApiResponse(404));

            return Ok(order);
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderUseCase.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}