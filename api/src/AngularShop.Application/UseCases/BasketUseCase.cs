using System.Threading.Tasks;
using AngularShop.Application.Dtos.Basket;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities;
using AngularShop.Core.Interfaces;
using AutoMapper;

namespace AngularShop.Application.UseCases
{
    public class BasketUseCase : IBasketUseCase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketUseCase(IBasketRepository basketRepository,
                             IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketToReturn> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetAsync(basketId);
            return _mapper.Map<Basket, BasketToReturn>(basket);
        }

        public async Task<BasketToReturn> UpdateBasketAsync(BasketRequest basketRequest)
        {
            var newBasket = _mapper.Map<BasketRequest, Basket>(basketRequest);

            var updatedBasket = await _basketRepository.UpdateAsync(newBasket);
            return _mapper.Map<Basket, BasketToReturn>(updatedBasket);
        }

        public async Task<bool> RemoveBasketAsync(string basketId)
        {
            return await _basketRepository.RemoveAsync(basketId);
        }
    }
}