using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebShop.Data.Models;

namespace WebShop.Data
{
    public class WebShopContext : DbContext
    {
        public WebShopContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string currentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
            string jsonFilePath = Path.Combine(currentDirectory, "WebShop.Data/ProductsInformationSeed.json");
            string jsonData = File.ReadAllText(jsonFilePath);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);

            // Seed data
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
