using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Core.Entities;

namespace AngularShop.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

        Task<Product> GetProductByIdAsync(int id);
    }
}