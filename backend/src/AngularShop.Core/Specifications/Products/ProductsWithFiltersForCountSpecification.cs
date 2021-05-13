using AngularShop.Core.Entities;

namespace AngularShop.Core.Specifications.Products
{
    public class ProductsWithFiltersForCountSpecification: BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductParamsSpec @params) :
                                                        base(x =>
                                                                    (string.IsNullOrWhiteSpace(@params.Search) 
                                                                        || x.Name.ToLower().Contains(@params.Search)
                                                                        || x.Description.ToLower().Contains(@params.Search)) &&
                                                                    (!@params.BrandId.HasValue || x.ProductBrandId == @params.BrandId) &&
                                                                    (!@params.TypeId.HasValue || x.ProductTypeId == @params.TypeId)
                                                            )
        {
            
        }
    }
}