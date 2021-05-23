using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularShop.Application.Dtos.Order;
using AngularShop.Application.Services.Accessors;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities;
using AngularShop.Core.Entities.Order;
using AngularShop.Core.Interfaces.Repositories;
using AngularShop.Core.Specifications.Orders;
using AutoMapper;

namespace AngularShop.Application.UseCases
{
    public class OrderUseCase : IOrderUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderUseCase(IMapper mapper,
                            IUserAccessor userAccessor,
                            IUnitOfWork unitOfWork,
                            IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderRequest orderRequest)
        {
            var email = _userAccessor.GetUser().FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<OrderAddressRequest, OrderAddress>(orderRequest.ShipToAddress);

            var basket = await _basketRepository.GetAsync(orderRequest.BasketId);
            if (basket == null) return null;

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var orderProduct = new OrderProduct(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(orderProduct, product.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderRequest.DeliveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var order = new Order(items, email, address, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return null;

            await _basketRepository.RemoveAsync(orderRequest.BasketId);

            return _mapper.Map<Order, OrderResponse>(order);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(int id)
        {
            var email = _userAccessor.GetUser().FindFirstValue(ClaimTypes.Email);
            var spec = new OrdersWithItemsAndOrderingSpecification(id, email);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            return _mapper.Map<Order, OrderResponse>(order);
        }

        public async Task<IReadOnlyList<OrderResponse>> GetOrdersFromBuyerAsync()
        {
            var email = _userAccessor.GetUser().FindFirstValue(ClaimTypes.Email);
            var spec = new OrdersWithItemsAndOrderingSpecification(email);
            var orders = await _unitOfWork.Repository<Order>().ListAsync(spec);

            return _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderResponse>>(orders);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }
    }
}