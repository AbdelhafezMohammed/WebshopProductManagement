using Microsoft.AspNetCore.Mvc;
using WebShop.Service.Dtos;
using WebShop.Service.Services;

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> Get([FromServices] IProductsService productsService)
        {
            var products = await productsService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> Create([FromServices] IProductsService productsService, ProductDto productDto)
        {
            var product = await productsService.CreateProductsAsync(productDto);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(product);
        }

        [HttpPut(Name = "UpdateProduct")]
        public async Task<IActionResult> Update([FromServices] IProductsService productsService, ProductDto productDto)
        {
            var product = await productsService.UpdateProductAsync(productDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
