using AngularShop.Application.Dtos;
using AngularShop.Core.Entities;
using AngularShop.Core.Settings;
using AutoMapper;

namespace AngularShop.Application.Mappers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly ApiSettingsData _apiSettingsData;

        public ProductUrlResolver(ApiSettingsData apiSettingsData)
        {
            _apiSettingsData = apiSettingsData;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return _apiSettingsData.ApiUrl + source.PictureUrl;
            }

            return null;
        }
    }
}