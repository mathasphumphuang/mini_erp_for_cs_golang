namespace CoreErpService.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; } // 💡 บังคับต้องมี ID ตอนอัปเดต
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}