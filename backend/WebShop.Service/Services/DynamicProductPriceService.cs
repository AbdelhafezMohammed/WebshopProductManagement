using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebShop.Tests")]
namespace WebShop.Service.Services
{
    internal class DynamicProductPriceService
    {
        private const int LowStockthreshold = 10;
        private const int HighStockthreshold = 100;
        private const int IncreasePricePercentage = 20;
        private const int DecreasePricePercentage = 5;

        internal double StockBasedPriceAdjuster(double price, int quantity)
        {
            double finalPrice = 0;

            if (quantity == 0)
            {
                return 0;
            }

            if (quantity < LowStockthreshold)
            {
                finalPrice = price + (price * IncreasePricePercentage / 100);
            }
            else if (quantity > HighStockthreshold)
            {
                finalPrice = price - (price * DecreasePricePercentage / 100);
            }
            return finalPrice;
        }
    }
}
