using AngularShop.Application.Dtos.Address;
using AngularShop.Application.Dtos.Basket;
using AngularShop.Application.Dtos.Order;
using AngularShop.Application.Dtos.Product;
using AngularShop.Core.Entities;
using AngularShop.Core.Entities.Identity;
using AngularShop.Core.Entities.Order;
using AutoMapper;

namespace AngularShop.Application.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<BasketRequest, Basket>();
            CreateMap<BasketItemRequest, BasketItem>();

            CreateMap<Basket, BasketResponse>();
            CreateMap<BasketItem, BasketItemResponse>();

            CreateMap<AddressRequest, Address>();
            CreateMap<Address, AddressResponse>();

            CreateMap<OrderAddressRequest, OrderAddress>();
            CreateMap<OrderAddress, AddressResponse>();
            CreateMap<Order, OrderResponse>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
                .ForMember(d => d.ShipToAddress, o => o.MapFrom(s => s.ShipToAddress));
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.OrderProduct.ProductId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OrderProduct.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}