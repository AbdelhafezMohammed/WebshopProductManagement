using WebShop.Data.Models;

namespace WebShop.Data.Repositories
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAsync();
        Task AddAsync(Product product);
        void UpdateProductAsync(Product product);
        Task SaveChangesAsync();
        Task<Product?> GetByIdAsync(object id);
    }
}
