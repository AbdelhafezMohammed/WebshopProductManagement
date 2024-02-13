namespace WebShop.Service.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? AdjustedPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
