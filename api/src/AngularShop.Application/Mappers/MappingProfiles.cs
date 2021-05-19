using AngularShop.Application.Dtos.Address;
using AngularShop.Application.Dtos.Basket;
using AngularShop.Application.Dtos.Product;
using AngularShop.Core.Entities;
using AngularShop.Core.Entities.Identity;
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
        }
    }
}