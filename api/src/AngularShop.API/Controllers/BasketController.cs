using System.Threading.Tasks;
using AngularShop.Application.Dtos.Basket;
using AngularShop.Application.UseCases.Gateways;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularShop.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly ILogger<BasketController> _logger;
        private readonly IBasketUseCase _basketUseCase;
        public BasketController(ILogger<BasketController> logger,
                                IBasketUseCase basketUseCase)
        {
            _logger = logger;
            _basketUseCase = basketUseCase;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketResponse>> GetBasketById(string id)
        {
            var basket = await _basketUseCase.GetBasketAsync(id);

            return Ok(basket ?? new BasketResponse(id));
        }

        [HttpPost]
        public async Task<ActionResult<BasketResponse>> UpdateBasket(BasketRequest basketRequest)
        {
            var updatedBasket = await _basketUseCase.UpdateBasketAsync(basketRequest);
            return Ok(updatedBasket);
        }

        [HttpDelete("{id}")]
        public async Task RemoveBasket(string id)
        {
            await _basketUseCase.RemoveBasketAsync(id);
        }
    }
}