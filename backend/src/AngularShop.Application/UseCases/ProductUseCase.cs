using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngularShop.Application.Dtos;
using AngularShop.Application.Page;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities;
using AngularShop.Core.Interfaces;
using AngularShop.Core.Specifications.Products;
using AutoMapper;

namespace AngularShop.Application.UseCases
{
    public class ProductUseCase : IProductUseCase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductUseCase(IGenericRepository<Product> productRepository,
                                IGenericRepository<ProductBrand> productBrandRepository,
                                IGenericRepository<ProductType> productTypeRepository,
                                IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<ProductToReturnDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetEntityWithSpec(new ProductsWithTypesAndBrandsSpecification(id));
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductParamsSpec @params)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(@params);
            var countSpec = new ProductsWithFiltersForCountSpecification(@params);

            var products = await _productRepository.ListAsync(spec);
            var totalProducts = await _productRepository.CountAsync(countSpec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var response = new Pagination<ProductToReturnDto>
            {
                PageSize = @params.PageSize,
                PageIndex = @params.PageIndex,
                TotalItemsPage = products.Count,
                TotalPages = (int)Math.Ceiling(totalProducts * 1.00M / @params.PageSize),
                TotalItems = totalProducts,
                Data = data
            };

            return response;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _productBrandRepository.ListAllAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _productTypeRepository.ListAllAsync();
        }
    }
}