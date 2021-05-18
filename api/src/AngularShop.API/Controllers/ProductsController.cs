using System.Threading.Tasks;
using AngularShop.Application.Dtos.Product;
using AngularShop.Application.Errors;
using AngularShop.Application.Page;
using AngularShop.Application.UseCases.Gateways;
using AngularShop.Core.Entities;
using AngularShop.Core.Specifications.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AngularShop.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductUseCase _productUseCase;

        public ProductsController(ILogger<ProductsController> logger,
                                  IProductUseCase productUseCase)
        {
            _logger = logger;
            _productUseCase = productUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductParamsSpec @params)
        {
            var response = await _productUseCase.GetProductsAsync(@params);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productUseCase.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            var productBrands = await _productUseCase.GetProductBrandsAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<Product>> GetProductTypes()
        {
            var productTypes = await _productUseCase.GetProductTypesAsync();
            return Ok(productTypes);
        }
    }
}