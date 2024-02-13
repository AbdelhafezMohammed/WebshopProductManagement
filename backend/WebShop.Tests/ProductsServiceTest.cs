using WebShop.Service.Services; 

namespace WebShop.Tests
{
    [TestClass]
    public class ProductsServiceTest
    {
        [TestMethod]
        public void TestDynamicProductPrice_Decrease()
        {
            //Arrange 
            double price = 105;
            int quantity = 5;

            //Act
            DynamicProductPriceService service = new DynamicProductPriceService();
            var adjustedFinalPrice = service.StockBasedPriceAdjuster(price, quantity);

            //Assert
            Assert.AreEqual(126, adjustedFinalPrice);
        }

        [TestMethod]
        public void TestDynamicProductPrice_Increase()
        {
            //Arrange 
            double price = 60;
            int quantity = 200;

            //Act
            DynamicProductPriceService service = new DynamicProductPriceService();
            var adjustedFinalPrice = service.StockBasedPriceAdjuster(price, quantity);

            //Assert
            Assert.AreEqual(57, adjustedFinalPrice);
        }

        [TestMethod]
        public void TestDynamicProductPrice_Fail()
        {
            //Arrange 
            double price = 60;
            int quantity = 0;

            //Act
            DynamicProductPriceService service = new DynamicProductPriceService();
            var adjustedFinalPrice = service.StockBasedPriceAdjuster(price, quantity);

            //Assert
            Assert.AreEqual(0, adjustedFinalPrice);
        }
    }
}