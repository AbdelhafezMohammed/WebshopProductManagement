using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebShop.Data.Models;
using WebShop.Service.Services;

namespace WebShop.Service.Interceptors
{
    public class AdjustProductPriceInterceptor : ISaveChangesInterceptor
    {
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            var entries = eventData?.Context?.ChangeTracker.Entries().ToList();
            var dbContext = eventData.Context;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Product)
                    {
                        var product = (Product)entry.Entity;
                        DynamicProductPriceService service = new DynamicProductPriceService();
                        //var resultProduct = dbContext.Set<Product>().Find(product.Id);
                        product.AdjustedPrice = service.StockBasedPriceAdjuster(product.Price, product.StockQuantity);
                        dbContext.SaveChanges();
                    }
                }
            }
            return new ValueTask<InterceptionResult<int>>();
        }
    }
}

