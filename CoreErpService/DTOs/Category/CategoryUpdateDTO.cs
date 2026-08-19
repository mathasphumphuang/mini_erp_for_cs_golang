namespace CoreErpService.DTOs.Category
{
    public class CategoryUpdateDTO
    {
        public int Id { get; set; } // 💡 อัปเดตต้องใช้ ID อ้างอิงเสมอ
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}