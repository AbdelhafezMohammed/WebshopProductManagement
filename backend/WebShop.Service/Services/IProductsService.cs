using WebShop.Data.Models;
using WebShop.Service.Dtos;

namespace WebShop.Service.Services
{
    public interface IProductsService
    {
        Task<List<ProductDto>> GetProductsAsync();
        Task<Product?> CreateProductsAsync(ProductDto productDto);
        Task<Product?> UpdateProductAsync(ProductDto productDto);
    }
}
