using AngularShop.Application.Dtos.Order;
using AngularShop.Application.Dtos.Product;
using AngularShop.Core.Entities;
using AngularShop.Core.Entities.Order;
using AngularShop.Core.Settings;
using AutoMapper;

namespace AngularShop.Application.Mappers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemResponse, string>
    {
        private readonly ApiSettingsData _apiSettingsData;

        public OrderItemUrlResolver(ApiSettingsData apiSettingsData)
        {
            _apiSettingsData = apiSettingsData;
        }
        public string Resolve(OrderItem source, OrderItemResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.OrderProduct.PictureUrl))
                return _apiSettingsData.ApiUrl + source.OrderProduct.PictureUrl;

            return null;
        }
    }
}