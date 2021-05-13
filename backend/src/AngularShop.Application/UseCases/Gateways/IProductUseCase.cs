using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Application.Dtos;
using AngularShop.Application.Page;
using AngularShop.Core.Entities;
using AngularShop.Core.Specifications.Products;

namespace AngularShop.Application.UseCases.Gateways
{
    public interface IProductUseCase
    {
        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductParamsSpec @params);
        Task<ProductToReturnDto> GetProductByIdAsync(int id);

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}