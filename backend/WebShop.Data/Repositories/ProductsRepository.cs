using Microsoft.EntityFrameworkCore;
using WebShop.Data.Models;

namespace WebShop.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly WebShopContext _context;

        public ProductsRepository(WebShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(object id)
        {
            return await _context.Products.FindAsync(new[] { id });
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
