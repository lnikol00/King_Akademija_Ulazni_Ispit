using DEV_Test.Controllers.DTO;
using DEV_Test.Services.ProductService;
using DEV_Test.Services.ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEV_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<ResultModel>> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProducts();
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel>> GetProductById(int id)
        {
            var singleProduct = await _productService.GetProductById(id);

            if (singleProduct is null)
            {
                return NotFound($"Product with id:{id} doesn't exist!");
            }
            else
            {
                return Ok(singleProduct);
            }
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<ResultModel>> GetFilterProducts([FromQuery] SearchRequestDTO searchRequest)
        {
            var filteredProduct = await _productService.GetFilterProducts(searchRequest);
            return Ok(filteredProduct);
        }

        [HttpGet("Search")]
        public async Task<ActionResult<ResultModel>> GetProductsBySearch([FromQuery] string search = "")
        {
            var searchProducts = await _productService.GetProductsBySearch(search);
            return Ok(searchProducts);
        }
    }
}
