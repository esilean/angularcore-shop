using AngularShop.Application.Dtos.Basket;
using AngularShop.Application.Dtos.Product;
using AngularShop.Core.Entities;
using AutoMapper;

namespace AngularShop.Application.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(x => x.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(x => x.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<BasketRequest, Basket>();
            CreateMap<BasketItemRequest, BasketItem>();

            CreateMap<Basket, BasketToReturn>();
            CreateMap<BasketItem, BasketItemToReturn>();
        }
    }
}