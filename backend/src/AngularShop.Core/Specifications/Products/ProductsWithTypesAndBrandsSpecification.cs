using System;
using System.Linq.Expressions;
using AngularShop.Core.Entities;

namespace AngularShop.Core.Specifications.Products
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductParamsSpec @params) : 
                                                        base(x =>
                                                                    (string.IsNullOrWhiteSpace(@params.Search) 
                                                                        || x.Name.ToLower().Contains(@params.Search)
                                                                        || x.Description.ToLower().Contains(@params.Search)) &&
                                                                    (!@params.BrandId.HasValue || x.ProductBrandId == @params.BrandId) &&
                                                                    (!@params.TypeId.HasValue || x.ProductTypeId == @params.TypeId)
                                                            )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            
            ApplyPaging(@params.PageSize * (@params.PageIndex - 1), @params.PageSize);

            if(!string.IsNullOrWhiteSpace(@params.Sort))
            {
                switch(@params.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}